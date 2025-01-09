// using RabbitMQ.Client;
// using System.Text;


// namespace Send
// {
//     public class Sender
//     {
//         public static async Task Main()
//         {
//             var factory = new ConnectionFactory()
//             {
//                 HostName = "localhost",
//                 UserName = "guest",
//                 Password = "guest"
//             };

//             using var connection = await factory.CreateConnectionAsync();
//             using var channel = await connection.CreateChannelAsync();

//             await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
//                 arguments: null);

//             const string message = "Hello World!";
//             var body = Encoding.UTF8.GetBytes(message);

//             await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
//             Console.WriteLine($" [x] Sent {message}");

//             Console.WriteLine(" Press [enter] to exit.");
//             Console.ReadLine();
//         }
//     }
// }

