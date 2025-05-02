namespace BasketAPI.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCart).NotNull().WithMessage("ShoppingCart is required.");
        RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("UserName is required.");
        RuleFor(x => x.ShoppingCart.Items).NotEmpty().WithMessage("Items are required.");
        RuleForEach(x => x.ShoppingCart.Items).ChildRules(item =>
        {
            item.RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
            item.RuleFor(x => x.ProductName).NotEmpty().WithMessage("ProductName is required.");
            item.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
            item.RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        });
    }
}

internal class StoreBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await basketRepository.StoreBasket(command.ShoppingCart, cancellationToken);

        return new StoreBasketResult(result.UserName);
    }
}