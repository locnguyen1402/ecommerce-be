using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using ECommerce.Shared.Data.Extensions;

using ECommerce.Inventory.Data;

namespace ECommerce.Inventory.DbMigrator;

public class InventoryDbContextFactory : IDesignTimeDbContextFactory<InventoryDbContext>
{
    public InventoryDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<InventoryDbContext> optionsBuilder = new();

        optionsBuilder.UseMigrationDatabase<InventoryDbContextFactory>(nameof(InventoryDbContext));

        return new InventoryDbContext(optionsBuilder.Options);
    }

    /*
     * cd db-migrators/Inventory.DbMigrator
     * dotnet ef migrations add AddMerchantCollection -c ECommerce.Inventory.Data.InventoryDbContext -o PostgreSQL/Migrations
     *
     * dotnet ef migrations script -i -c ECommerce.Inventory.Data.InventoryDbContext -o PostgreSQL/Scripts/000_Snapshot.sql
     *
     * dotnet ef migrations script -i -c ECommerce.Inventory.Data.InventoryDbContext UpdateProductVariantAttributeValue AddMerchantCollection -o PostgreSQL/Scripts/006_AddMerchantCollection.sql
     */
}
