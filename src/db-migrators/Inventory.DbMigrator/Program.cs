using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ECommerce.Shared.Data.Extensions;
using ECommerce.Shared.Common.Constants;

using ECommerce.Inventory.Domain.AggregatesModel.Identity;

using ECommerce.Inventory.Data;
using ECommerce.Inventory.DbMigrator.Seeds;

namespace ECommerce.Inventory.DbMigrator;

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

                services.AddDbContext<InventoryDbContext>((serviceProvider, options) =>
                {
                    options.UseMigrationDatabase<InventoryDbContext>(connectionString);
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

        await host.MigrateDbContext<InventoryDbContext>(Seeds.Inventory.Seed_Release_001.SeedAsync);
        await host.MigrateDbContext<IdentityDbContext>(Seeds.Identity.Seed_Release_001.SeedAsync);

        Environment.Exit(-1);
    }
}
