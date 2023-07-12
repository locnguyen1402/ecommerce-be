using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Libs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var entryAssembly = Assembly.GetEntryAssembly() ?? throw new NullReferenceException("entryAssembly");

        return services.AddAutoMapper(entryAssembly);
    }

    public static IServiceCollection ConfigDbContext<TDbContext>(this IServiceCollection services, string connectionString) where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(option =>
        {
            option
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });

        return services;
    }
}