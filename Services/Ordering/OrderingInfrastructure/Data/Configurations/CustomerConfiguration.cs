

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderingInfrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(productId => productId.Value, value => CustomerId.Of(value));

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        builder.Property(c => c.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(c => c.Email).IsUnique();
    }
}
