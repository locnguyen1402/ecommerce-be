using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ECommerce.Shared.Data.Extensions;
using ECommerce.Shared.Common.Constants;

using ECommerce.Domain.AggregatesModel.Identity;

using ECommerce.Data;
using ECommerce.DbMigrator.Seeds;

namespace ECommerce.DbMigrator;

class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString(SchemaConstants.DATABASE_CONNECTION)!;

                services.AddDbContext<MigrationDbContext>((serviceProvider, options) =>
                {
                    options.UseMigrationDatabase<MigrationDbContext>(connectionString);
                });

                services.AddDbContext<ECommerceDbContext>((serviceProvider, options) =>
                {
                    options.UseMigrationDatabase<ECommerceDbContext>(connectionString);
                });


                services.AddDbContext<IdentityDbContext>((serviceProvider, options) =>
                {
                    options.UseMigrationDatabase<IdentityDbContext>(connectionString);
                });

                services.AddOpenIddict()
                    .AddCore(options =>
                    {
                        options.UseEntityFrameworkCore()
                            .UseDbContext<IdentityDbContext>()
                            .ReplaceDefaultEntities<Application, Authorization, Scope, Token, Guid>();
                    });

                services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<IdentityDbContext>()
                    .AddDefaultTokenProviders();

                services.AddDistributedMemoryCache();
                services.AddMemoryCache();
            })
            .Build();

        await host.MigrateDbContext<MigrationDbContext>();

        await host.MigrateDbContext<ECommerceDbContext>(Seeds.ECommerce.Seed_Release_001.SeedAsync);
        await host.MigrateDbContext<IdentityDbContext>(Seeds.Identity.Seed_Release_001.SeedAsync);

        Environment.Exit(-1);
    }
}
