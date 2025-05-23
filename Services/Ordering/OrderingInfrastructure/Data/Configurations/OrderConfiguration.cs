using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderingInfrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(orderId => orderId.Value, value => OrderId.Of(value));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.ComplexProperty(o => o.OrderName, orderName =>
        {
            orderName.Property(on => on.Value)
                .IsRequired()
                .HasMaxLength(5);
        });

        builder.ComplexProperty(o => o.ShippingAddress, shippingAddress =>
        {
            shippingAddress.Property(sa => sa.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            shippingAddress.Property(sa => sa.LastName)
                .IsRequired()
                .HasMaxLength(50);
            shippingAddress.Property(sa => sa.Email)
                .HasMaxLength(255);
            shippingAddress.Property(sa => sa.Street)
                .IsRequired()
                .HasMaxLength(100);
            shippingAddress.Property(sa => sa.City)
                .IsRequired()
                .HasMaxLength(50);
            shippingAddress.Property(sa => sa.State)
                .IsRequired()
                .HasMaxLength(50);
            shippingAddress.Property(sa => sa.ZipCode)
                .IsRequired()
                .HasMaxLength(20);
            shippingAddress.Property(sa => sa.Country)
                .IsRequired()
                .HasMaxLength(50);
        });

        builder.ComplexProperty(o => o.BillingAddress, billingAddress =>
        {
            billingAddress.Property(sa => sa.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            billingAddress.Property(sa => sa.LastName)
                .IsRequired()
                .HasMaxLength(50);
            billingAddress.Property(sa => sa.Email)
                .HasMaxLength(255);
            billingAddress.Property(sa => sa.Street)
                .IsRequired()
                .HasMaxLength(100);
            billingAddress.Property(sa => sa.City)
                .IsRequired()
                .HasMaxLength(50);
            billingAddress.Property(sa => sa.State)
                .IsRequired()
                .HasMaxLength(50);
            billingAddress.Property(sa => sa.ZipCode)
                .IsRequired()
                .HasMaxLength(20);
            billingAddress.Property(sa => sa.Country)
                .IsRequired()
                .HasMaxLength(50);
        });

        builder.ComplexProperty(o => o.Payment, payment =>
        {
            payment.Property(p => p.CardNumber)
                .IsRequired()
                .HasMaxLength(16);
            payment.Property(p => p.CardName)
                .IsRequired()
                .HasMaxLength(100);
            payment.Property(p => p.Expiration)
                .IsRequired()
                .HasMaxLength(5);
            payment.Property(p => p.Cvv)
                .IsRequired()
                .HasMaxLength(3);
            payment.Property(p => p.PaymentMethod)
                .IsRequired();
        });


        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                status => status.ToString(),
                value => Enum.Parse<OrderStatus>(value));

        builder.Property(o => o.TotalPrice)
            .HasPrecision(18, 2);
    }
}
