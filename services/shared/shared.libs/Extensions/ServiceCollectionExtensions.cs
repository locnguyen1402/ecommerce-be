using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Libs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        return services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}