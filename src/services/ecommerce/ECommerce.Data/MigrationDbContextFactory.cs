using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data;

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
