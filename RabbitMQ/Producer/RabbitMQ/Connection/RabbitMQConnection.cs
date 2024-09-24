using RabbitMQ.Client;

namespace Producer.RabbitMQ.Connection
{
    public class RabbitMQConnection : IRabbitMQConnection, IDisposable
    {
        public IConnection? _connection { get; }
        public IConnection Connection => _connection!;




        public RabbitMQConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                ClientProvidedName = "Producer",
            };

            _connection = factory.CreateConnection();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

    }
}
