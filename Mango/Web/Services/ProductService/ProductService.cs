using Web.Models;
using Web.Util;

namespace Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpService _httpService;

        public ProductService(IHttpService httpService)
        {
            _httpService = httpService;
        }


        //=======================================================================================================
        public async Task<ResponseResult> CreateProduct(ProductDto Product)
        {
            RequestDTO request = new RequestDTO
            {
                Url = Shared.ProductApi + "/api/Products",
                ApiType = ApiType.POST,
                Body = Product
            };

            return await _httpService.SendAsync(request);
        }

        //=======================================================================================================
        public async Task<ResponseResult> UpdateProduct(ProductDto Product)
        {
            RequestDTO request = new RequestDTO
            {
                Url = Shared.ProductApi + "/api/Products",
                ApiType = ApiType.PUT,
                Body = Product
            };

            return await _httpService.SendAsync(request);
        }

        //=======================================================================================================
        public async Task<ResponseResult> DeleteProduct(string code)
        {
            RequestDTO request = new RequestDTO
            {
                Url = $"{Shared.ProductApi}/api/Products/{code}",
                ApiType = ApiType.DELETE,
            };

            return await _httpService.SendAsync(request);
        }

        //=======================================================================================================
        public async Task<ResponseResult> GetAllProducts()
        {
            RequestDTO request = new RequestDTO
            {
                Url = Shared.ProductApi + "/api/Products",
                ApiType = ApiType.GET,
            };

            return await _httpService.SendAsync(request);
        }

        //=======================================================================================================
        public async Task<ResponseResult> GetProductById(int id)
        {
            RequestDTO request = new RequestDTO
            {
                Url = $"{Shared.ProductApi}/api/Products/{id}",
                ApiType = ApiType.GET,
            };

            return await _httpService.SendAsync(request);
        }

    }
}
