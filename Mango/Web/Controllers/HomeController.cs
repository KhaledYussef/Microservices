using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System.Diagnostics;

using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _ProductService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _ProductService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _ProductService.GetAllProducts();
            if (result.IsSuccess)
            {
                var Products = JsonConvert.DeserializeObject<List<ProductDto>>(result.Data?.ToString()!);
                return View(Products);
            }
            else
            {
                TempData["error"] = result.Message;
            }

            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _ProductService.GetProductById(id);
            if (response.IsSuccess)
            {
                var Product = JsonConvert.DeserializeObject<ProductDto>(response.Data?.ToString()!);
                return View(Product);
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
