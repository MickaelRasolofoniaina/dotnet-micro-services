namespace OrderingDomain.Abstractions;

public interface IAggregate : IEntity
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    public void AddDomainEvent(IDomainEvent domainEvent);
    public void RemoveDomainEvent(IDomainEvent domainEvent);
    public void ClearDomainEvents();
}

public interface IAggregate<T> : IAggregate, IEntity<T>
{

}