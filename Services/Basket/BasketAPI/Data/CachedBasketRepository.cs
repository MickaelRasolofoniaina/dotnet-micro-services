using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace BasketAPI.Data;

public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache distributedCache) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
    {
        var cachedBasket = await distributedCache.GetStringAsync(userName, cancellationToken);

        if (cachedBasket != null)
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        }

        var basket = await basketRepository.GetBasket(userName, cancellationToken);

        await distributedCache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken)
    {
        await basketRepository.StoreBasket(shoppingCart, cancellationToken);

        await distributedCache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart), cancellationToken);

        return shoppingCart;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
    {
        await basketRepository.DeleteBasket(userName, cancellationToken);

        await distributedCache.RemoveAsync(userName, cancellationToken);

        return true;
    }
}
