
namespace BuildingBlocks.Exceptions;

public class NotFoundException<TEntity>(Guid id) : Exception($"Entity {typeof(TEntity).Name} with id {id} was not found.")
{
}
