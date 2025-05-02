
namespace BasketAPI.Data;

public class BasketRepository(IDocumentSession documentSession) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
    {
        var basket = await documentSession.LoadAsync<ShoppingCart>(userName, cancellationToken) ?? throw new BasketNotFoundException(userName);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken)
    {
        documentSession.Store(shoppingCart);

        await documentSession.SaveChangesAsync(cancellationToken);

        return shoppingCart;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
    {
        documentSession.Delete<ShoppingCart>(userName);

        await documentSession.SaveChangesAsync(cancellationToken);

        return true;
    }
}
