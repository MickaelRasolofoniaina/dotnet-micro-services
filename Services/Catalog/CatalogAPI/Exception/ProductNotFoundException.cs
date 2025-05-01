using BuildingBlocks.Exceptions;

namespace CatalogAPI.Exception;

public class ProductNotFoundException(Guid id) : NotFoundException("Product", id)
{
}
