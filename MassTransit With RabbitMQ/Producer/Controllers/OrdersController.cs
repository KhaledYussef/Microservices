using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Producer.Models;
using Producer.Services;

namespace Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order model)
        {
            try
            {
                var order = await _orderService.SaveOrder(model);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
