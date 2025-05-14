using DiscountAPI;

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

internal class StoreBasketCommandHandler(IBasketRepository basketRepository, DiscountAPI.DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await ApplyDiscount(command.ShoppingCart, cancellationToken);

        var result = await basketRepository.StoreBasket(command.ShoppingCart, cancellationToken);

        return new StoreBasketResult(result.UserName);
    }

    private async Task<ShoppingCart> ApplyDiscount(ShoppingCart shoppingCart, CancellationToken cancellationToken)
    {
        foreach (var item in shoppingCart.Items)
        {
            var discountRequest = new GetDiscountRequest
            {
                ProductName = item.ProductName,
            };

            var discountResponse = await discountProtoServiceClient.GetDiscountAsync(discountRequest, cancellationToken: cancellationToken);

            if (discountResponse != null)
            {
                item.Price -= discountResponse.Amount;
            }
        }

        return shoppingCart;
    }
}