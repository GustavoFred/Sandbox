using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infrastructure.RabbitMQ
{

    public class RabbitMQProducer
    {
        private readonly RabbitMQConnection _connection;
        private readonly IConfiguration _config;

        public RabbitMQProducer(RabbitMQConnection connection, IConfiguration config)
        {
            _connection = connection;
            _config = config;
        }

        public async Task Publish<T>(T message)
        {
            var channel = await _connection.CreateChannel();

            var queue = _config["RabbitMq:Queue"];

            await channel.QueueDeclareAsync(
                queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            var props = new BasicProperties
            {
                Persistent = true
            };


            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queue,
                mandatory: false,
                basicProperties: props,
                body: body
            );
        }
    }
}
