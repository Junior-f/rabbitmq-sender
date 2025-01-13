using System;
using System.Threading.Tasks;

namespace Sender.Service
{
    public interface IRabbitMqService
    {
        Task SendMessage(string routingKey, string exchange, object message);
    }
}
