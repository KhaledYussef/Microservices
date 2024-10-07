using Web.Models;
using Web.Util;

namespace Web.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpService _http;
        public CartService(IHttpService baseService)
        {
            _http = baseService;
        }

        public async Task<ResponseResult?> ApplyCouponAsync(CartDTO cartDto)
        {
            return await _http.SendAsync(new RequestDTO()
            {
                ApiType = ApiType.POST,
                Body = cartDto,
                Url = Shared.CartApi + "/api/cart/ApplyCoupon"
            });
        }

        public async Task<ResponseResult?> EmailCart(CartDTO cartDto)
        {
            return await _http.SendAsync(new RequestDTO()
            {
                ApiType = ApiType.POST,
                Body = cartDto,
                Url = Shared.CartApi + "/api/cart/EmailCartRequest"
            });
        }

        public async Task<ResponseResult?> GetCartByUserIdAsnyc(string userId)
        {
            return await _http.SendAsync(new RequestDTO()
            {
                ApiType = ApiType.GET,
                Url = Shared.CartApi + "/api/cart/GetCart/" + userId
            });
        }


        public async Task<ResponseResult?> RemoveFromCartAsync(int cartDetailsId)
        {
            return await _http.SendAsync(new RequestDTO()
            {
                ApiType = ApiType.POST,
                Body = cartDetailsId,
                Url = Shared.CartApi + "/api/cart/RemoveCart"
            });
        }


        public async Task<ResponseResult?> UpsertCartAsync(CartDTO cartDto)
        {
            return await _http.SendAsync(new RequestDTO()
            {
                ApiType = ApiType.POST,
                Body = cartDto,
                Url = Shared.CartApi + "/api/cart/Upsert"
            });
        }
    }
}
