using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Sender.Models;

namespace Sender.Service
{
    //     public class ReceiverService: IReceiverService
    //     {
    //         private readonly IConfiguration _configuration;

    //         public ReceiverService(IConfiguration configuration)
    //         {
    //             _configuration = configuration;
    //             var factory = new ConnectionFactory()
    //             {
    //                 HostName = "localhost",
    //                 UserName = "guest",
    //                 Password = "guest",

    //             };

    //             // var queueName = "hello";

    //             // Start(factory);
    //         }

    //         public async Task Start(ConnectionFactory factory)
    //         {
    //             Console.WriteLine($"configuration >>>> {_configuration}");

    //             var queueName = _configuration["RabbitMQ:QueueName"] ?? "hello";


    //             IConnection connection = await factory.CreateConnectionAsync();
    //             IChannel channel = await connection.CreateChannelAsync();

    //             // await channel.QueueDeclareAsync(queueName, false, false, false, null);
    //             await channel.ExchangeDeclareAsync(queueName, ExchangeType.Direct);
    //             await channel.QueueBindAsync(queueName, queueName, "");

    //             var consumer = new AsyncEventingBasicConsumer(channel);

    //             consumer.ReceivedAsync += async (ch, ea) =>
    //             {
    //                 var body = ea.Body.ToArray();
    //                 var message = Encoding.UTF8.GetString(body);

    //                 Console.WriteLine($"[x] Received: {message}");

    //                 var order = JsonSerializer.Deserialize<Order>(message);
    //                 await channel.BasicAckAsync(ea.DeliveryTag, false);
    //             };

    //             await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);

    //             Console.WriteLine(" Press [enter] to exit.");
    //             Console.ReadLine();
    //         }
    //     }

    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using Sender.Models;

    public class Receiver : BackgroundService
    {
        private readonly ILogger<Receiver> _logger;
        private IConnection _connection;
        private IChannel _channel;

        private readonly string _queueName = "ordem_de_compra";

        public Receiver(ILogger<Receiver> logger)
        {
            _logger = logger;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;

            _channel.QueueDeclareAsync(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RabbitMQ Consumer iniciado.");

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation($"Mensagem recebida: {message}");

                var order = JsonSerializer.Deserialize<Product>(message);
                await _channel.BasicAckAsync(ea.DeliveryTag, false);

            };

            _channel.BasicConsumeAsync(queue: _queueName, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
            base.Dispose();
        }
    }

}
