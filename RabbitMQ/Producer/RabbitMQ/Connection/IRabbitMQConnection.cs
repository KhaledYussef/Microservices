using RabbitMQ.Client;

namespace Producer.RabbitMQ.Connection
{
    public interface IRabbitMQConnection
    {
        IConnection Connection { get; }
    }
}
