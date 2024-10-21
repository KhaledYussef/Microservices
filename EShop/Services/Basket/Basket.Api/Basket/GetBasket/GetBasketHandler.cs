


namespace Basket.Api.Basket.GetBasket
{
    public record GetBasketQuery(string UserId) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart Cart);
    public class GetBasketQueryHandler(IBasketRepository basketRepository)
        : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(query.UserId, cancellationToken);
            return new GetBasketResult(basket ?? new ShoppingCart(query.UserId));

        }
    }
}
