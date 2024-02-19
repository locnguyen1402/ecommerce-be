using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ECommerce.Shared.Data.Extensions;
using ECommerce.Shared.Common.Constants;

using ECommerce.Catalog.Data;

namespace ECommerce.Catalog.DbMigrator;

class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString(SchemaConstants.DATABASE_CONNECTION)!;

                services.AddDbContext<CatalogDbContext>((serviceProvider, options) =>
                {
                    options.UseMigrationDatabase<CatalogDbContext>(connectionString);
                });
            })
            .Build();

        await host.MigrateDbContext<CatalogDbContext>();

        Environment.Exit(-1);
    }
}
