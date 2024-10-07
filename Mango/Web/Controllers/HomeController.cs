using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System.Diagnostics;
using System.Security.Claims;

using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _ProductService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _ProductService = productService;
            _cartService = cartService;
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

        [Authorize]
        [HttpPost]
        [ActionName("Details")]
        public async Task<IActionResult> Details(ProductDto productDto)
        {
            CartDTO cartDto = new CartDTO()
            {
                    UserId = User.Claims.Where(u => u.Type == ClaimTypes.Name)?.FirstOrDefault()?.Value
                
            };

            CartDetailDTO cartDetails = new CartDetailDTO()
            {
                Count = productDto.Count,
                ProductId = productDto.Id,
            };

            List<CartDetailDTO> cartDetailsDtos = new() { cartDetails };
            cartDto.Items = cartDetailsDtos;

            ResponseResult? response = await _cartService.UpsertCartAsync(cartDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Item has been added to the Shopping Cart";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(productDto);
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
