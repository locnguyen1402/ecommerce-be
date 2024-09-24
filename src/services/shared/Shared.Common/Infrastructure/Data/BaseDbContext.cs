using Microsoft.EntityFrameworkCore;

namespace ECommerce.Shared.Common.Infrastructure.Data;

public abstract class BaseDbContext(DbContextOptions opts) : DbContext(opts)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // var entryAssembly = Assembly.GetCallingAssembly() ?? throw new NullReferenceException("entryAssembly");

        // modelBuilder.ApplyConfigurationsFromAssembly(entryAssembly);
    }
}