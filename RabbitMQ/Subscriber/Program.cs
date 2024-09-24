using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using Shared;

using System.Text;

Console.WriteLine("Hello, World!");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    ClientProvidedName = "Subscriber",
};

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();
channel.QueueDeclare(queue: Queues.Orders, exclusive: false);
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
var consumer = new EventingBasicConsumer(channel);

consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(" [x] Received {0}", message);
    channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
};

channel.BasicConsume(queue: Queues.Orders, autoAck: false, consumer: consumer);


Console.ReadLine();

