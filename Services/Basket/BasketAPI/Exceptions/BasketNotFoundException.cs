namespace BasketAPI.Exceptions;

public class BasketNotFoundException(string userName) : NotFoundException("Basket", userName)
{
}
