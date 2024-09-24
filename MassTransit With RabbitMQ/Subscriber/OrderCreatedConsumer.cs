using MassTransit;

using Shared.Models;

namespace Subscriber
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(context.Message);
            Console.WriteLine($"Order created: {json}");

            await Task.CompletedTask;
        }
    }
}
