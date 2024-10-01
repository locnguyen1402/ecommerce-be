using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using ECommerce.Shared.Data.Extensions;

using ECommerce.Inventory.Data;

namespace ECommerce.Inventory.DbMigrator;

public class DbContextFactory : IDesignTimeDbContextFactory<MigrationDbContext>
{
    public MigrationDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<MigrationDbContext> optionsBuilder = new();

        optionsBuilder.UseMigrationDatabase<DbContextFactory>(nameof(MigrationDbContext));

        return new MigrationDbContext(optionsBuilder.Options);
    }

    /*
     * cd db-migrators/Inventory.DbMigrator
     * dotnet ef migrations add UpdateCode -c ECommerce.Inventory.Data.MigrationDbContext -o PostgreSQL/Migrations
     *
     * dotnet ef migrations script -i -c ECommerce.Inventory.Data.MigrationDbContext -o PostgreSQL/Scripts/000_Snapshot.sql
     *
     * dotnet ef migrations script -i -c ECommerce.Inventory.Data.MigrationDbContext UpdateProductSkuAndCode UpdateCode -o PostgreSQL/Scripts/004_UpdateCode.sql
     */
}
