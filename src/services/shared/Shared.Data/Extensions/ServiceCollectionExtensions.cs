using Npgsql;

using System.Security.Cryptography.X509Certificates;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerce.Shared.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IHostApplicationBuilder ConfigPooledDbContext<TDbContext, TDbContextFactory>(
        this IHostApplicationBuilder builder,
        string connectionName,
        int poolSize = 1024)
        where TDbContext : DbContext
        where TDbContextFactory : class, IDbContextFactory<TDbContext>
    {
        var connectionString = builder.Configuration.GetConnectionString(connectionName)!;
        var dataSourceBuilder = new NpgsqlSlimDataSourceBuilder(connectionString);

        dataSourceBuilder.EnableDynamicJson(jsonbClrTypes: [typeof(IReadOnlyCollection<string>)]);
        dataSourceBuilder.EnableArrays();

        var dataSource = dataSourceBuilder.Build();

        builder.AddPooledDbContext<TDbContext, TDbContextFactory>(options => options.UseDatabase(dataSource), poolSize);

        return builder;
    }

    public static IServiceCollection AddDataProtectionContext<TDbContext>(
        this IServiceCollection services,
        string applicationName,
        int lifetime = 90,
        X509Certificate2[]? certs = null)
        where TDbContext : DbContext, IDataProtectionKeyContext
    {
        var builder = services.AddDataProtection()
            .SetApplicationName(applicationName)
            .SetDefaultKeyLifetime(TimeSpan.FromDays(lifetime))
            .PersistKeysToDbContext<TDbContext>();

        if (certs == null || certs.Length < 2)
            return services;

        builder.ProtectKeysWithCertificate(certs[0])
            .UnprotectKeysWithAnyCertificate(certs[1..]);

        return services;
    }

    public static IHostApplicationBuilder AddPooledDbContext<TDbContext, TDbContextFactory>(
        this IHostApplicationBuilder builder, Action<DbContextOptionsBuilder> optionsAction, int poolSize = 1024)
        where TDbContext : DbContext
        where TDbContextFactory : class, IDbContextFactory<TDbContext>
    {
        #region DbContext

        // 1. First, register a pooling context factory as a Singleton service, as usual
        builder.Services
            .AddPooledDbContextFactory<TDbContext>(optionsAction, poolSize);

        // 2. Register an additional context factory as a Scoped service,
        // which gets a pooled context from the Singleton factory we registered above,
        // finds the required services (e.g. ILogger), and injects them into the context
        builder.Services.AddScoped<TDbContextFactory>();

        // 3. Finally, arrange for a context to get injected from our Scoped factory
        builder.Services.AddScoped(
            sp => sp.GetRequiredService<TDbContextFactory>().CreateDbContext());

        #endregion

        return builder;
    }

    public static IHostApplicationBuilder AddPooledDbContext<TDbContext, TDbContextFactory>(
        this IHostApplicationBuilder builder, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction, int poolSize = 1024)
        where TDbContext : DbContext
        where TDbContextFactory : class, IDbContextFactory<TDbContext>
    {
        #region DbContext

        // 1. First, register a pooling context factory as a Singleton service, as usual
        builder.Services
            .AddPooledDbContextFactory<TDbContext>(optionsAction, poolSize);

        // 2. Register an additional context factory as a Scoped service,
        // which gets a pooled context from the Singleton factory we registered above,
        // finds the required services (e.g. ILogger), and injects them into the context
        builder.Services.AddScoped<TDbContextFactory>();

        // 3. Finally, arrange for a context to get injected from our Scoped factory
        builder.Services.AddScoped(
            sp => sp.GetRequiredService<TDbContextFactory>().CreateDbContext());

        #endregion

        return builder;
    }
}
