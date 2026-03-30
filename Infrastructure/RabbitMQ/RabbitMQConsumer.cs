using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Infrastructure.RabbitMQ
{
    public class RabbitMQConsumer
    {
        private readonly RabbitMQConnection _connection;
        private readonly IConfiguration _config;

        public RabbitMQConsumer(RabbitMQConnection connection, IConfiguration config)
        {
            _config = config;
            _connection = connection;
        }

        public async Task StartConsuming<T>(Func<T, Task> onMessage)
        {
            var channel = await _connection.CreateChannel();

            var queue = _config["RabbitMq:Queue"];

            await channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);

                    var message = JsonSerializer.Deserialize<T>(json);

                    if (message != null)
                    {
                        await onMessage(message);
                    }

                    await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    await channel.BasicNackAsync(
                        ea.DeliveryTag,
                        multiple: false,
                        requeue: true
                    );

                    throw new Exception($"Erro ao processar mensagem: {ex.Message}");
                }
            };

            await channel.BasicConsumeAsync(queue: queue, autoAck: false, consumer: consumer);

        }

        public async Task<List<string>> GetAllMessages()
        {
            var channel = await _connection.CreateChannel();

            var queue = _config["RabbitMq:Queue"];

            var messages = new List<string>();

            while (true)
            {
                var result = await channel.BasicGetAsync(queue, autoAck: false);

                if (result == null)
                    break;

                var body = result.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                messages.Add(message);

                await channel.BasicAckAsync(result.DeliveryTag, multiple: false);
            }

            return messages;
        }
    }
}
