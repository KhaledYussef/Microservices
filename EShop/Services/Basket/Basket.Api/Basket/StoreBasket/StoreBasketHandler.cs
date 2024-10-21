namespace Basket.Api.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(bool IsSuccess);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull();
            RuleFor(x => x.Cart.UserName).NotEmpty();
            RuleFor(x => x.Cart.Items).Must(x => x.Any()).WithMessage("No items in the basket");

        }
    }

    public class StoreBasketCommandHandler(IBasketRepository basketRepository)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await basketRepository.StoreBasket(command.Cart);
            return new StoreBasketResult(true);
        }
    }
}
