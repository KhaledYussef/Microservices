using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IReportStorage _reportStorage;

        public HomeController(IReportStorage reportStorage)
        {
            _reportStorage = reportStorage;
        }

        // get reports 
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_reportStorage.Get());
        }

    }
}
