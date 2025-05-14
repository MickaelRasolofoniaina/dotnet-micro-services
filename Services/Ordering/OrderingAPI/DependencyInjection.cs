

namespace OrderingAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Add your API services here
        return services;
    }

    public static WebApplication UseServices(this WebApplication app)
    {
        // Configure your API middleware here
        return app;
    }
}
