using System.Text;
using System.Threading.Tasks;
using IMDBAPI.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace IMDBAPI.Services
{
    public class MessageService<T> : IMessageService<T> where T : class
    {
        private RabbitMQConnection _connection;

        public MessageService(IOptions<RabbitMQConnection> connection)
        {
            _connection = connection.Value;
        }

        public async Task PostMessageAsync(T message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _connection.HostName,
                UserName = _connection.UserName,
                Password = _connection.Password
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var messageJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageJson);

            await Task.Run(() => channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body));
        }

    }
}
