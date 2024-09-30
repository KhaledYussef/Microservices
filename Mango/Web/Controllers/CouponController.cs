using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Newtonsoft.Json;

using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _couponService.GetAllCoupons();
            if (result.IsSuccess)
            {
                var coupons = JsonConvert.DeserializeObject<List<CouponDto>>(result.Data?.ToString()!);
                return View(coupons);
            }
            else
            {
                TempData["error"] = result.Message;
            }

            return View();
        }

        public IActionResult CreateCoupon()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CouponDto model)
        {
            if (!ModelState.IsValid)
                return View();

            var response = await _couponService.CreateCoupon(model);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("fdfds", response.Message ?? "Error While Saving");
                TempData["error"] = response.Message ?? "Error While Saving";
                return View();
            }

            return RedirectToAction(nameof(Index));


        }
    }
}
