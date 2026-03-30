using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Infrastructure
{
    public class RabbitMQConnection
    {
        private IConnection? _connection;
        private IConfiguration _config;

        public RabbitMQConnection(IConfiguration config)
        {
            _config = config;
        }

        public async Task InitializeAsync(IConfiguration config)
        {
            var factory = new ConnectionFactory()
            {
                HostName = config["RabbitMq:Host"]
            };

            _connection = await factory.CreateConnectionAsync();
        }

        public async Task<IChannel> CreateChannel()
        {
            await InitializeAsync(_config);

            if (_connection == null)
                throw new Exception("Could not open RabbitMQ connection");

            return await _connection.CreateChannelAsync();
        }
    }
}
