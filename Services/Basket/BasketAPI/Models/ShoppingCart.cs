using Marten.Schema;

namespace BasketAPI.Models;

public class ShoppingCart
{
    [Identity]
    public string UserName { get; set; } = string.Empty;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    public int TotalItems => Items.Sum(item => item.Quantity);

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
    public ShoppingCart()
    {
    }
}
