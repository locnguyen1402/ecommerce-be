using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Libs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var entryAssembly = Assembly.GetEntryAssembly() ?? throw new NullReferenceException("entryAssembly");
        
        return services.AddAutoMapper(entryAssembly);
    }
}