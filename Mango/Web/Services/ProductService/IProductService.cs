
using Web.Models;

namespace Web.Services
{
    public interface IProductService
    {
        Task<ResponseResult> CreateProduct(ProductDto Product);
        Task<ResponseResult> DeleteProduct(string code);
        Task<ResponseResult> GetAllProducts();
        Task<ResponseResult> GetProductById(int id);
        Task<ResponseResult> UpdateProduct(ProductDto Product);
    }
}