namespace CatalogAPI.Exception;

public class ProductNotFoundException(Guid id) : System.Exception($"Product with id {id} not found.")
{
}
