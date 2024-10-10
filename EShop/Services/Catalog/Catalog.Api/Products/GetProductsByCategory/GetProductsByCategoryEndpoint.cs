
namespace Catalog.Api.Products.GetProductsByCategory
{
    //public record GetProductsByCategoryRequest(string category);
    public record GetProductsByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/Category/{cat}", async (string cat, ISender sender) =>
            {
                var query = new GetProductsByCategoryQuery(cat);
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductsByCategoryResponse>();
                return response;
            })
            .WithName("GetProductsByCategory");
        }
    }
}
