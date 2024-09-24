using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OTM.InMemoryUserService;

namespace OTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreetController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IInMemoryUserService _inMemoryUserService;
        public StreetController(HttpClient httpClient,
            IInMemoryUserService inMemoryUserService)
        {
            _httpClient = httpClient;
            _inMemoryUserService = inMemoryUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = _inMemoryUserService.GetUsers();
            var response = await _httpClient.GetAsync("https://localhost:7071/WeatherForecast");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            return BadRequest();
        }
    }
}
