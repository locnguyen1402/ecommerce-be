using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ECommerce.Shared.Common.Constants;
using ECommerce.Shared.Common.EventBus.Data;
using ECommerce.Shared.Data.Extensions;

namespace ECommerce.EventBus.DbMigrator;

class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString(SchemaConstants.DATABASE_CONNECTION)!;

                services.AddDbContext<EventBusDbContext>((serviceProvider, options) =>
                {
                    options.UseMigrationDatabase<EventBusDbContext>(connectionString);
                });
            })
            .Build();

        await host.MigrateDbContext<EventBusDbContext>();

        Environment.Exit(-1);
    }
}
