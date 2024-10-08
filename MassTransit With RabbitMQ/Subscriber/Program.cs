﻿
using MassTransit;

using Subscriber;

var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{

    cfg.ReceiveEndpoint("order-created-event", e =>
    {
        e.Consumer<OrderCreatedConsumer>();
        e.PrefetchCount = 1;
    });

    
});

await busControl.StartAsync(new CancellationToken());

try
{
    Console.WriteLine("Press enter to exit");
    await Task.Run(() => Console.ReadLine());
}
finally
{
    await busControl.StopAsync(new CancellationToken());
}