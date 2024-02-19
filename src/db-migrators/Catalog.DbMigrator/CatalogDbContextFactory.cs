using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using ECommerce.Shared.Data.Extensions;

using ECommerce.Catalog.Data;

namespace ECommerce.Catalog.DbMigrator;

public class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
{
    public CatalogDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<CatalogDbContext> optionsBuilder = new();

        optionsBuilder.UseMigrationDatabase<CatalogDbContextFactory>(nameof(CatalogDbContext));

        return new CatalogDbContext(optionsBuilder.Options);
    }

    /*
     * cd db-migrators/Catalog.DbMigrator
     * dotnet ef migrations add Initialize -c ECommerce.Catalog.Data.CatalogDbContext -o PostgreSQL/Migrations
     *
     * dotnet ef migrations script -i -c ECommerce.Catalog.Data.CatalogDbContext -o PostgreSQL/Scripts/000_Snapshot.sql
     *
     * dotnet ef migrations script -i -c ECommerce.Catalog.Data.CatalogDbContext 0 Initialize -o PostgreSQL/Scripts/010_Initialize.sql
     */
}
