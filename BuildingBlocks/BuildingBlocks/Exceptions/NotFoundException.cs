namespace BuildingBlocks.Exceptions;

public class NotFoundException(string entity, object key) : Exception($"Entity {entity} with id {key} was not found.")
{
}
