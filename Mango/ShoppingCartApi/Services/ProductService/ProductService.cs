using Newtonsoft.Json;

using ShoppingCartApi.Models.Dto;

namespace ShoppingCartApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Get Products
        public async Task<IEnumerable<ProductDto>?> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync("api/products");

            var apiContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseResult>(apiContent);

            if (result is null)
                return null;

            if (result.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(result.Data?.ToString()!);
            }

            return null;
        }

    }
}
