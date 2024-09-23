using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Data;

public class MigrationDbContextFactory(
    IDbContextFactory<MigrationDbContext> pooledFactory
) : IDbContextFactory<MigrationDbContext>
{
    public MigrationDbContext CreateDbContext()
    {
        var context = pooledFactory.CreateDbContext();

        return context;
    }
}
