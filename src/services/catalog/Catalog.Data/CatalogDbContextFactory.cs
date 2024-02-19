using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.Data;

public class CatalogDbContextFactory(
    IDbContextFactory<CatalogDbContext> pooledFactory
) : IDbContextFactory<CatalogDbContext>
{
    public CatalogDbContext CreateDbContext()
    {
        var context = pooledFactory.CreateDbContext();

        return context;
    }
}
