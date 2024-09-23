using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using ECommerce.Shared.Data.Extensions;

using ECommerce.Inventory.Data;

namespace ECommerce.Inventory.DbMigrator;

public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<IdentityDbContext> optionsBuilder = new();

        optionsBuilder.UseMigrationDatabase<IdentityDbContextFactory>(nameof(IdentityDbContext));

        return new IdentityDbContext(optionsBuilder.Options);
    }

    /*
    * cd db-migrators/Identity.DbMigrator
    * dotnet ef migrations add InitializeIdentity -c ECommerce.Inventory.Data.IdentityDbContext -o PostgreSQL/Migrations/Identity
    *
    * dotnet ef migrations script -i -c ECommerce.Inventory.Data.IdentityDbContext -o PostgreSQL/Scripts/Identity/000_Snapshot.sql
    *
    * dotnet ef migrations script -i -c ECommerce.Inventory.Data.IdentityDbContext 0 InitializeIdentity -o PostgreSQL/Scripts/Identity/001_InitializeIdentity.sql
    */
}

