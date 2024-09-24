using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Plain.RabbitMQ;

using System.Text.Json;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPublisher _publisher;
        public OrderController(IPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            _publisher.Publish(order.ToString(), "inventory.neworder", null);
            return StatusCode(StatusCodes.Status201Created);
        }
    }

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
