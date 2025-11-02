using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace OrderingInfrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.MigrateAsync();

        await SeedAsync(dbContext);
    }

    private static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        await SeedCustomerAsync(dbContext);
        await SeedProductAsync(dbContext);
        await SeedOrderAndItemsAsync(dbContext);
    }

    private static async Task SeedCustomerAsync(ApplicationDbContext dbContext)
    {
        if (dbContext.Customers.Any())
        {
            return;
        }

        dbContext.Customers.AddRange(InitialData.Customers);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedProductAsync(ApplicationDbContext dbContext)
    {
        if (dbContext.Products.Any())
        {
            return;
        }

        dbContext.Products.AddRange(InitialData.Products);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedOrderAndItemsAsync(ApplicationDbContext dbContext)
    {
        if (dbContext.Orders.Any())
        {
            return;
        }

        dbContext.Orders.AddRange(InitialData.Orders);
        await dbContext.SaveChangesAsync();
    }
}
