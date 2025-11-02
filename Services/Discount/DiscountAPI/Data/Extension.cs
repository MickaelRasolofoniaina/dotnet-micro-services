using Microsoft.EntityFrameworkCore;

namespace DiscountAPI.Data;

public static class Extension
{
    public static IApplicationBuilder UseAutoMigrations(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var services = scope.ServiceProvider;

            using var context = services.GetRequiredService<DiscountContext>();

            context.Database.MigrateAsync();
        }

        return app;
    }
}
