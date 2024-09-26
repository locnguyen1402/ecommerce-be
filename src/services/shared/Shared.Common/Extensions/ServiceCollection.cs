using System.Reflection;

using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Amazon.S3;
using Amazon;

using FluentValidation;
using FluentValidation.AspNetCore;

using Npgsql;

using ECommerce.Shared.Common.Constants;
using ECommerce.Shared.Common.Infrastructure.Settings;
using ECommerce.Shared.Common.Data.Interceptors;

namespace ECommerce.Shared.Libs.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureJson(this IServiceCollection services)
    {
        services
            .Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.DefaultIgnoreCondition = JsonConstant.JsonSerializerOptions.DefaultIgnoreCondition;
                options.SerializerOptions.PropertyNamingPolicy = JsonConstant.JsonSerializerOptions.PropertyNamingPolicy;
                options.SerializerOptions.DictionaryKeyPolicy = JsonConstant.JsonSerializerOptions.DictionaryKeyPolicy;
                options.SerializerOptions.ReferenceHandler = JsonConstant.JsonSerializerOptions.ReferenceHandler;
            });

        return services;
    }
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var assemblies = GetAssemblies();

        return services.AddAutoMapper(assemblies);
    }
    public static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        var assemblies = GetAssemblies();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies);
        });

        return services;
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
        var dataSourceBuilder = new NpgsqlSlimDataSourceBuilder(connectionString);
        dataSourceBuilder.EnableDynamicJson(jsonbClrTypes: [typeof(IReadOnlyCollection<string>)]);
        dataSourceBuilder.EnableArrays();

        var dataSource = dataSourceBuilder.Build();

        services.AddDbContext<TDbContext>(options =>
        {
            options.UseNpgsql(
                dataSource,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(assembly);
                }).UseSnakeCaseNamingConvention();

            options.AddInterceptors(new SaveChangesInterceptor());
        });

        // Old version
        // services.AddDbContext<TDbContext>(option =>
        // {
        //     option
        //         .UseNpgsql(connectionString, opt =>
        //         {
        //             opt.MigrationsAssembly(assembly);
        //         })
        //         .UseSnakeCaseNamingConvention();
        // });

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

    public static IServiceCollection AddObjectStorageService(this IServiceCollection services, AwsSettings settings)
    {
        services.AddSingleton(Options.Create(settings));

        services.AddSingleton<IAmazonS3>(_ =>
        {
            return new AmazonS3Client(settings.AccessKey, settings.SecretKey, new AmazonS3Config()
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(settings.Region),
                ServiceURL = settings.ServiceUrl,
            });

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