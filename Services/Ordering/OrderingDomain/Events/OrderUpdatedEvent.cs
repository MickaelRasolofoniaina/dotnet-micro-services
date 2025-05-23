namespace OrderingDomain.Events;

public record OrderUpdatedEvent(Order Order) : IDomainEvent
{

}
