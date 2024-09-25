using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Reflection;

using Writer.Services;

namespace Writer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WritersController(IWriterRepository writerRepository) : ControllerBase
    {
        private readonly IWriterRepository _writerRepository = writerRepository;

        [HttpPost]
        public IActionResult Post([FromBody] Models.Writer writer)
        {
            var result = _writerRepository.Insert(writer);

            return Created($"/get/{result.Id}", result);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_writerRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _writerRepository.Get(id);
            Thread.Sleep(2000);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
