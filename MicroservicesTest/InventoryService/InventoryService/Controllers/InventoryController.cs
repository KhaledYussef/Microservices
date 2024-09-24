using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryStorage _inventoryStorage;

        public InventoryController(IInventoryStorage inventoryStorage)
        {
            _inventoryStorage = inventoryStorage;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_inventoryStorage.GetAllItems());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _inventoryStorage.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Item item)
        {
            _inventoryStorage.AddItem(item);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Item item)
        {
            _inventoryStorage.UpdateItemQuantity(id, item.Quantity);
            return Ok();
        }
    }
}
