using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Data;

public class InventoryDbContextFactory(
    IDbContextFactory<InventoryDbContext> pooledFactory
) : IDbContextFactory<InventoryDbContext>
{
    public InventoryDbContext CreateDbContext()
    {
        var context = pooledFactory.CreateDbContext();

        return context;
    }
}
