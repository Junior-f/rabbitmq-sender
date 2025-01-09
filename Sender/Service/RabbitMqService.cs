using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Sender.Service
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly string _hostname;
        private readonly ConnectionFactory _factory;

        public RabbitMqService(string hostname)
        {
            _hostname = hostname;
            _factory = new ConnectionFactory() { HostName = _hostname };
        }
        public async Task SendMessage(string queueName, object mensagem)
        {
            try
            {
                using var connection = await _factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();
                await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);


                var message = JsonConvert.SerializeObject(mensagem);
                var body = Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync(exchange: "topic", routingKey: queueName, body: body);

                Console.WriteLine($"[RabbitMQ] Mensagem enviada para a fila '{queueName}': {message}");
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RabbitMQ] Erro ao enviar mensagem: {ex.Message}");
                throw;
            }
        }
    }
}

