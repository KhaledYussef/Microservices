using Web.Models;
using Web.Util;

namespace Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly IHttpService _http;

        public CouponService(IHttpService http)
        {
            _http = http;
        }

        public async Task<ResponseResult> GetCouponByCode(string code)
        {
            RequestDTO request = new RequestDTO
            {
                Url = $"{Shared.CouponApi}/api/coupon/GetByCode/{code}",
                ApiType = ApiType.GET,
          
            };

            return await _http.SendAsync(request);
        }

        //=======================================================================================================
        public async Task<ResponseResult> CreateCoupon(CouponDto coupon)
        {
            RequestDTO request = new RequestDTO
            {
                Url = Shared.CouponApi + "/api/coupon",
                ApiType = ApiType.POST,
                Body = coupon
            };

            return await _http.SendAsync(request);
        }

        //=======================================================================================================
        public async Task<ResponseResult> UpdateCoupon(CouponDto coupon)
        {
            RequestDTO request = new RequestDTO
            {
                Url = Shared.CouponApi + "/api/coupon",
                ApiType = ApiType.PUT,
                Body = coupon
            };

            return await _http.SendAsync(request);
        }

        //=======================================================================================================
        public async Task<ResponseResult> DeleteCoupon(string code)
        {
            RequestDTO request = new RequestDTO
            {
                Url = $"{Shared.CouponApi}/api/coupon/{code}",
                ApiType = ApiType.DELETE,
            };

            return await _http.SendAsync(request);
        }

        //=======================================================================================================
        public async Task<ResponseResult> GetAllCoupons()
        {
            RequestDTO request = new RequestDTO
            {
                Url = Shared.CouponApi + "/api/coupon",
                ApiType = ApiType.GET,
            };

            return await _http.SendAsync(request);
        }

        //=======================================================================================================
        public async Task<ResponseResult> GetCouponById(int id)
        {
            RequestDTO request = new RequestDTO
            {
                Url = $"{Shared.CouponApi}/api/coupon/{id}",
                ApiType = ApiType.GET,
            };

            return await _http.SendAsync(request);
        }
    }
}
