using Producer.RabbitMQ.Connection;

using RabbitMQ.Client;

using Shared;

using System.Text.Json;

namespace Producer.RabbitMQ
{
    public class RabbitMqProducer : IMessageProducer
    {
        private readonly IRabbitMQConnection _connection;

        public RabbitMqProducer(IRabbitMQConnection connection)
        {
            _connection = connection;
        }

        public void SendMessage<T>(T message)
        {
            using var channel = _connection.Connection.CreateModel();
            channel.QueueDeclare(queue: Queues.Orders, exclusive: false);

            var json = JsonSerializer.Serialize(message);
            var body = System.Text.Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: Queues.Orders, basicProperties: null, body: body);
        }
    }
}
