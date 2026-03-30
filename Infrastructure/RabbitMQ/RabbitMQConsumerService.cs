using Microsoft.Extensions.Hosting;

namespace Infrastructure.RabbitMQ
{
    public class RabbitMQConsumerService : BackgroundService
    {
        private readonly RabbitMQConsumer _consumer;

        public RabbitMQConsumerService(RabbitMQConsumer consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.StartConsuming<string>(async (msg) =>
            {
                Console.WriteLine($"Recebido: {msg}");
                await Task.CompletedTask;
            });
        }
    }
}
