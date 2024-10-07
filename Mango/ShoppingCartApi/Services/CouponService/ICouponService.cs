using ShoppingCartApi.Models.Dto;

namespace ShoppingCartApi.Services
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}