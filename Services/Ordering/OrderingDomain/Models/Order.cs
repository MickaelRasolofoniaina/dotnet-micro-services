namespace OrderingDomain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = [];
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public OrderName OrderName { get; private set; } = default!;
    public CustomerId CustomerId { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice => _orderItems.Sum(item => item.UnitPrice * item.Quantity);

    public static Order Create(OrderId id, OrderName name, CustomerId customerId, Address shippingAddress, Address billingAddress, Payment payment)
    {
        var order = new Order
        {
            Id = id,
            OrderName = name,
            CustomerId = customerId,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending
        };

        order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }

    public void Update(OrderName name, Address shippingAddress, Address billingAddress, Payment payment)
    {
        OrderName = name;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;

        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void AddOrderItem(ProductId productId, int quantity, decimal unitPrice)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(unitPrice, nameof(unitPrice));

        var orderItem = new OrderItem(Id, productId, quantity, unitPrice);
        _orderItems.Add(orderItem);
    }

    public void RemoveOrderItem(OrderItemId orderItemId)
    {
        var orderItem = _orderItems.FirstOrDefault(item => item.Id == orderItemId);

        if (orderItem is not null)
        {
            _orderItems.Remove(orderItem);
        }
    }
}
