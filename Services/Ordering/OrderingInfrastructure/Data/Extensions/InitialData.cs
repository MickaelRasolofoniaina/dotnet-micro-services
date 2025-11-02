namespace OrderingInfrastructure.Data.Extensions;

internal class InitialData
{
    public static readonly List<Customer> Customers =
    [
        Customer.Create(CustomerId.Of(new Guid("11111111-1111-1111-1111-111111111111")), "John", "john.doe@example.com"),
        Customer.Create(CustomerId.Of(new Guid("22222222-2222-2222-2222-222222222222")), "Jane", "jane.smith@example.com")
    ];

    public static readonly List<Product> Products =
    [
        Product.Create(ProductId.Of(new Guid("11111111-1111-1111-1111-111111111111")), "Laptop",  12),
        Product.Create(ProductId.Of(new Guid("22222222-2222-2222-2222-222222222222")), "Smartphone", 24)
    ];

    public static List<Order> Orders
    {
        get
        {
            var address1 = Address.Of("John", "Doe", "john.doe@example.com", "123 Main St", "New York", "NY", "10001", "USA");
            var address2 = Address.Of("Jane", "Smith", "jane.smith@example.com", "456 Park Ave", "Los Angeles", "CA", "90001", "USA");

            var payment1 = Payment.Of("John Doe", "4111111111111111", "12/25", "123", 1);
            var payment2 = Payment.Of("Jane Smith", "4222222222222222", "11/24", "456", 2);

            var order1 = Order.Create(OrderId.Of(new Guid("11111111-1111-1111-1111-111111111111")), OrderName.Of("12345"), CustomerId.Of(new Guid("11111111-1111-1111-1111-111111111111")), address1, address1, payment1);

            order1.AddOrderItem(ProductId.Of(new Guid("11111111-1111-1111-1111-111111111111")), 1, 999);
            order1.AddOrderItem(ProductId.Of(new Guid("22222222-2222-2222-2222-222222222222")), 2, 200);

            var order2 = Order.Create(OrderId.Of(new Guid("22222222-2222-2222-2222-222222222222")), OrderName.Of("67890"), CustomerId.Of(new Guid("22222222-2222-2222-2222-222222222222")), address2, address2, payment2);

            order2.AddOrderItem(ProductId.Of(new Guid("11111111-1111-1111-1111-111111111111")), 3, 150);
            order2.AddOrderItem(ProductId.Of(new Guid("22222222-2222-2222-2222-222222222222")), 1, 250);

            return
            [
               order1,
               order2
            ];
        }
        set { }
    }
}
