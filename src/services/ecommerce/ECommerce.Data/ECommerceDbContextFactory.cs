using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data;

public class ECommerceDbContextFactory(
    IDbContextFactory<ECommerceDbContext> pooledFactory
) : IDbContextFactory<ECommerceDbContext>
{
    public ECommerceDbContext CreateDbContext()
    {
        var context = pooledFactory.CreateDbContext();

        return context;
    }
}
