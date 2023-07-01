using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Shared.Common;

public abstract class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions opts) : base(opts)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}