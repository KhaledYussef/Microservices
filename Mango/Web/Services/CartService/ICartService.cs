
using Web.Models;

namespace Web.Services
{
    public interface ICartService
    {
        Task<ResponseResult?> ApplyCouponAsync(CartDTO cartDto);
        Task<ResponseResult?> EmailCart(CartDTO cartDto);
        Task<ResponseResult?> GetCartByUserIdAsnyc(string userId);
        Task<ResponseResult?> RemoveFromCartAsync(int cartDetailsId);
        Task<ResponseResult?> UpsertCartAsync(CartDTO cartDto);
    }
}