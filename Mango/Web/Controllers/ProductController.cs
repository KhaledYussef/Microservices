using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _ProductService;

        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
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

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto model)
        {
            if (!ModelState.IsValid)
                return View();

            var response = await _ProductService.CreateProduct(model);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("fdfds", response.Message ?? "Error While Saving");
                TempData["error"] = response.Message ?? "Error While Saving";
                return View();
            }

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var response = await _ProductService.GetProductById(id);
            if (!response.IsSuccess)
            {
                TempData["error"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            var Product = JsonConvert.DeserializeObject<ProductDto>(response.Data?.ToString()!);
            return View(Product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductDto model)
        {
            if (!ModelState.IsValid)
                return View();

            var response = await _ProductService.UpdateProduct(model);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("fdfds", response.Message ?? "Error While Saving");
                TempData["error"] = response.Message ?? "Error While Saving";
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _ProductService.GetProductById(id);
            if (!response.IsSuccess)
            {
                TempData["error"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            var Product = JsonConvert.DeserializeObject<ProductDto>(response.Data?.ToString()!);
            return View(Product);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductDto model)
        {
            var response = await _ProductService.DeleteProduct(model.Id.ToString());
            if (!response.IsSuccess)
            {
                TempData["error"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
