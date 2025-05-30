namespace BasketAPI.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart ShoppingCart);

internal class GetBasketQueryHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var result = await basketRepository.GetBasket(query.UserName, cancellationToken);

        return new GetBasketResult(result);
    }
}
