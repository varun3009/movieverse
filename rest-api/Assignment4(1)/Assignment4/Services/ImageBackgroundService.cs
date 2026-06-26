using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using IMDBAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class ImageBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    private readonly IMDBAPI.RabbitMQConnection _rabbitMqConnection;
    private IConnection? _connection;
    private IModel? _channel;

    public ImageBackgroundService(IServiceScopeFactory scopeFactory, IMovieRepository movieRepository, IMapper mapper, IOptions<IMDBAPI.RabbitMQConnection> rabbitMqOptions)
    {
        _scopeFactory = scopeFactory;
        _movieRepository = movieRepository;
        _mapper = mapper;
        _rabbitMqConnection = rabbitMqOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _rabbitMqConnection.HostName,
                    UserName = _rabbitMqConnection.UserName,
                    Password = _rabbitMqConnection.Password,
                    DispatchConsumersAsync = true
                };

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                StartConsumer(_channel);

                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RabbitMQ consumer startup failed: {ex.Message}. Retrying in 5 seconds.");
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }

    private void StartConsumer(IModel channel)
    {
        channel.QueueDeclare(
            queue: "processed_poster",
            durable: true,
            exclusive: false,
            autoDelete: false
        );

        channel.BasicQos(0, 1, false);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.Received += async (sender, args) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(args.Body.ToArray());

                var movie = JsonSerializer.Deserialize<MovieResponse>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (movie == null)
                {
                    channel.BasicAck(args.DeliveryTag, false);
                    return;
                }

                using var scope = _scopeFactory.CreateScope();

                if (movie != null)
                {
                    _movieRepository.UpdateMoviePoster(movie.Poster, movie.Caption, movie.Id);
                }

                channel.BasicAck(args.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating poster: {ex.Message}");
                channel.BasicNack(args.DeliveryTag, false, false);
            }
        };

        channel.BasicConsume(
            queue: "processed_poster",
            autoAck: false,
            consumer: consumer
        );
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();

        base.Dispose();
    }
}
