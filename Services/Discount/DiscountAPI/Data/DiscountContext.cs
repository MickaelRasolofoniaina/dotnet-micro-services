using DiscountAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscountAPI.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon
            {
                Id = 1,
                ProductName = "Apple iPhone 14",
                Description = "IPhone Discount",
                Amount = 150
            },
            new Coupon
            {
                Id = 2,
                ProductName = "Sony Playstation 5",
                Description = "Playstation Discount",
                Amount = 100
            }
        );
    }
}
