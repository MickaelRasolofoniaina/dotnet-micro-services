using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrderingInfrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add your infrastructure services here
        return services;
    }
}
