import io
import json

import pika
from PIL import Image, ImageDraw, ImageFont
from dotenv import load_dotenv
import boto3
import os
import torch
import time
from transformers import BlipProcessor, BlipForConditionalGeneration

device = "cuda" if torch.cuda.is_available() else "cpu"

processor = BlipProcessor.from_pretrained("Salesforce/blip-image-captioning-base")
caption_model = BlipForConditionalGeneration.from_pretrained(
    "Salesforce/blip-image-captioning-base"
).to(device)


def get_image_caption(pil_image):
    """
    Takes a PIL image directly and returns a caption.
    No need to save image.jpg locally.
    """
    image = pil_image.convert("RGB")

    inputs = processor(
        images=image,
        return_tensors="pt"
    ).to(device)

    output_ids = caption_model.generate(
        **inputs,
        max_new_tokens=50
    )

    caption = processor.decode(output_ids[0], skip_special_tokens=True)
    return caption




load_dotenv()

s3 = boto3.client(
    "s3",
    aws_access_key_id=os.getenv("Access_Key"),
    aws_secret_access_key=os.getenv("Secret_Key"),
    region_name=os.getenv("Region")
)


def create_rabbitmq_connection():
    host = os.getenv("RABBITMQ_HOST", "localhost")
    user = os.getenv("RABBITMQ_USER", "guest")
    password = os.getenv("RABBITMQ_PASSWORD", "guest")
    credentials = pika.PlainCredentials(user, password)

    for attempt in range(1, 11):
        try:
            return pika.BlockingConnection(
                pika.ConnectionParameters(host=host, credentials=credentials)
            )
        except pika.exceptions.AMQPConnectionError:
            print(f"RabbitMQ is not ready yet, retrying ({attempt}/10)...")
            time.sleep(5)

    raise RuntimeError("Could not connect to RabbitMQ")


connection = create_rabbitmq_connection()
channel = connection.channel()

channel.queue_declare(queue='update_poster', durable=False)

channel.queue_declare(queue='processed_poster', durable=True)

def add_water_mark(img, width=400, height=200):
    watermark_text = "IMDB"

    img = img.resize((width, height))

    watermark = Image.new("RGBA", img.size, (255, 255, 255, 0))
    draw = ImageDraw.Draw(watermark)

    font_size = max(24, width // 18)

    try:
        font = ImageFont.truetype("arial.ttf", font_size)
    except:
        font = ImageFont.load_default()

    text_box = draw.textbbox((0, 0), watermark_text, font=font)
    text_width = text_box[2] - text_box[0]
    text_height = text_box[3] - text_box[1]

    x = width - text_width - 20
    y = height - text_height - 50

    draw.text(
        (x, y),
        watermark_text,
        font=font,
        fill=(255, 255, 255, 140)
    )

    result = Image.alpha_composite(img, watermark)

    # Save as JPG
    result = result.convert("L")
    return result

def callback(ch, method, properties, body):
    try:
        msg = json.loads(body)
        img_key = msg["Poster"]
        img_obj = s3.get_object(Bucket=os.getenv("Bucket"), Key=img_key)
        img_data = img_obj['Body'].read()
        img = Image.open(io.BytesIO(img_data)).convert("RGBA")
        watermarked_img = add_water_mark(img)
        buffer = io.BytesIO()
        watermarked_img.save(buffer, format="JPEG", quality=90)
        buffer.seek(0)
        s3.put_object(Bucket=os.getenv("Bucket"), Key=f"posters/processed/{os.path.basename(img_key)}", Body=buffer.getvalue(), ContentType='image/jpeg')
        ch.basic_ack(delivery_tag=method.delivery_tag)
        msg["Poster"] = f"posters/processed/{os.path.basename(img_key)}"
        caption = get_image_caption(img)
        msg["Caption"] = caption
        print(f"Processed and uploaded image for {img_key}")
        channel.basic_publish(exchange='', routing_key='processed_poster', body=json.dumps(msg))
    except Exception as e:
        print(f"Error processing message: {e}")
        ch.basic_nack(delivery_tag=method.delivery_tag, requeue=False)
    


channel.basic_consume(queue='update_poster',
                      on_message_callback=callback,
                      auto_ack=False)

print(' [*] Waiting for messages. To exit press CTRL+C')

channel.basic_qos(prefetch_count=1)
channel.start_consuming()
