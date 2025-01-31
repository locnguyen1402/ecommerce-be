﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ECommerce.Shared.Data.Extensions;
using ECommerce.Shared.Common.Constants;

using ECommerce.Inventory.Data;

namespace ECommerce.Inventory.DbMigrator;

class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString(SchemaConstants.DATABASE_CONNECTION)!;

                services.AddDbContext<InventoryDbContext>((serviceProvider, options) =>
                {
                    options.UseMigrationDatabase<InventoryDbContext>(connectionString);
                });
            })
            .Build();

        await host.MigrateDbContext<InventoryDbContext>();

        Environment.Exit(-1);
    }
}
