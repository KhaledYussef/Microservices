using ShoppingCartApi.Models.Dto;

namespace ShoppingCartApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>?> GetProducts();
    }
}