using System.Text.Json;

namespace InventoryService.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

        // to json
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
