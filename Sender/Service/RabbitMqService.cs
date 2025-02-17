using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Channels;

namespace Sender.Service
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly string _exchange = "new";
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        public RabbitMqService()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq.utilities",
                UserName = "user",
                Password = "-DCqZEF5|n%bwV*5]KJMQjJYsd=JEF-x"

            };
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
        }
        public async Task SendMessage(string queueName, string _routingKey, object mensagem)
        {
            try
            {
                //await _channel.ExchangeDeclareAsync(exchange: _exchange, type: ExchangeType.Direct, durable: true);

                await _channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                //await _channel.QueueBindAsync(queue: queueName,  routingKey: _routingKey);

                var type = mensagem.GetType();

                var message = JsonConvert.SerializeObject(mensagem);
                var body = Encoding.UTF8.GetBytes(message);

                Console.WriteLine($"{type.Name} Published ");

                await _channel.BasicPublishAsync(exchange: "", routingKey: queueName, body);

                Console.WriteLine($"[RabbitMQ] Mensagem enviada para a fila '{queueName}': {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RabbitMQ] Erro ao enviar mensagem: {ex.Message}");
                throw;
            }
        }
    }
}

