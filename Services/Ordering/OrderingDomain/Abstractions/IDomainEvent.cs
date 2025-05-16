
namespace OrderingDomain.Abstractions;

public interface IDomainEvent
{
    Guid EventId => Guid.NewGuid();
    string? EventType => GetType().AssemblyQualifiedName;
    public DateTime OccurredOn => DateTime.UtcNow;
}
