
namespace BuildingBlocks.Exceptions;

public class NotFoundException(string entity, Guid id) : Exception($"Entity {entity} with id {id} was not found.")
{
}
