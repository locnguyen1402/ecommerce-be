using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using ECommerce.Shared.Common.EventBus.Data;
using ECommerce.Shared.Data.Extensions;

namespace ECommerce.EventBus.DbMigrator;

public class EventBusDbContextFactory : IDesignTimeDbContextFactory<EventBusDbContext>
{
    public EventBusDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<EventBusDbContext> optionsBuilder = new();

        optionsBuilder.UseMigrationDatabase<EventBusDbContextFactory>(nameof(EventBusDbContext));

        return new EventBusDbContext(optionsBuilder.Options);
    }

    /*
     * cd db-migrators/EventBus.DbMigrator
     * dotnet ef migrations add Initialize -c ECommerce.Shared.Common.EventBus.Data.EventBusDbContext -o PostgreSQL/Migrations
     *
     * dotnet ef migrations script -i -c ECommerce.Shared.Common.EventBus.Data.EventBusDbContext -o PostgreSQL/Scripts/000_Snapshot.sql
     *
     * dotnet ef migrations script -i -c ECommerce.Shared.Common.EventBus.Data.EventBusDbContext 0 Initialize -o PostgreSQL/Scripts/001_Initialize.sql
     */
}
