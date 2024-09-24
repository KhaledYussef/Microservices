using InventoryService.Models;

using Plain.RabbitMQ;

using System.Text.Json;

namespace InventoryService
{
    public class InventoryTracker : IHostedService
    {
        private readonly IInventoryStorage _inventoryStorage;
        private readonly ISubscriber _subscriber;

        public InventoryTracker(IInventoryStorage inventoryStorage, ISubscriber subscriber)
        {
            _inventoryStorage = inventoryStorage;
            _subscriber = subscriber;
            _subscriber.Subscribe(OnMessageReceived);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _subscriber.Subscribe(OnMessageReceived);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _subscriber.Dispose();
            return Task.CompletedTask;
        }

        private bool OnMessageReceived(string message, IDictionary<string, object> headers)
        {
            var order = JsonSerializer.Deserialize<Order>(message);
            var itemId = int.Parse(order.ProductId);
            var quantity = order.Quantity;
    
            _inventoryStorage.UpdateItemQuantity(itemId, quantity);

            return true;
        }


    }
}
