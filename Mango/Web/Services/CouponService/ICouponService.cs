using Web.Models;

namespace Web.Services
{
    public interface ICouponService
    {
        Task<ResponseResult> CreateCoupon(CouponDto coupon);
        Task<ResponseResult> DeleteCoupon(string code);
        Task<ResponseResult> GetAllCoupons();
        Task<ResponseResult> GetCouponByCode(string code);
        Task<ResponseResult> GetCouponById(int id);
        Task<ResponseResult> UpdateCoupon(CouponDto coupon);
    }
}