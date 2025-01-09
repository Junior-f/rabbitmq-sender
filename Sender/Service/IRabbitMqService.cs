using System;
using System.Threading.Tasks;

namespace Sender.Service
{
    public interface IRabbitMqService
    {
        Task SendMessage(string topic, object message);
    }
}
