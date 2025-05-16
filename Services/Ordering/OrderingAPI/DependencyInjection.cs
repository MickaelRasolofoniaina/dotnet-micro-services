

namespace OrderingAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Add your API services here
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        // Configure your API middleware here
        return app;
    }
}
