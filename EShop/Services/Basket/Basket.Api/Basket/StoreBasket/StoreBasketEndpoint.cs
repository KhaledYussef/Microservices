
namespace Basket.Api.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(bool IsSuccess);
    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                var command = new StoreBasketCommand(request.Cart);
                var result = await sender.Send(command);
                var response = new StoreBasketResponse(result.IsSuccess);
                return Results.Ok(response);

            });

        }
    }
}
