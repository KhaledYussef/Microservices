using Newtonsoft.Json;

using ShoppingCartApi.Models.Dto;

namespace ShoppingCartApi.Services
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<CouponDto> GetCoupon(string couponCode)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Coupon");
                var response = await client.GetAsync($"/api/Coupon/GetByCode/{couponCode}");
                var apiContet = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseResult>(apiContet);
                if (resp != null && resp.IsSuccess)
                {
                    return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Data));
                }
                return new CouponDto();
            }
            catch (Exception ex)
            {
                return new CouponDto();
            }
        }
    }
}
