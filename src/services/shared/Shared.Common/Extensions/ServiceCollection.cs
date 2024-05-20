using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using FluentValidation;
using FluentValidation.AspNetCore;

namespace ECommerce.Shared.Libs.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var assemblies = GetAssemblies();

        return services.AddAutoMapper(assemblies);
    }
    public static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        var assemblies = GetAssemblies();

        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
    }
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        var assemblies = GetAssemblies();

        services.AddValidatorsFromAssemblies(assemblies);
        services.AddFluentValidationAutoValidation();

        return services;
    }

    public static IServiceCollection ConfigDbContext<TDbContext>(this IServiceCollection services, string connectionString, string assembly) where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(option =>
        {
            option
                .UseNpgsql(connectionString, opt =>
                {
                    opt.MigrationsAssembly(assembly);
                })
                .UseSnakeCaseNamingConvention();
        });

        return services;
    }

    public static IServiceCollection ConfigureSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(
            options =>
            {
                options.DescribeAllParametersInCamelCase();
                options.UseOneOfForPolymorphism();
            });

        return services;
    }

    private static Assembly[] GetAssemblies()
    {
        var entryAssembly = Assembly.GetEntryAssembly() ?? throw new NullReferenceException("entryAssembly");

        var assemblyNames = entryAssembly
            .GetReferencedAssemblies()
            .Append(entryAssembly.GetName());

        var assemblies = assemblyNames.Select(t =>
        {
            var assembly = Assembly.Load(t);

            return assembly;
        }).Where(t => t != null).ToArray();

        return assemblies;
    }
}