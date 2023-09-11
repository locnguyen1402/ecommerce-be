using System.Reflection;
using ECommerce.Services.Orders;
using ECommerce.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Orders;

public class OrderDbContext : BaseDbContext
{
    public DbSet<Order> Orders => Set<Order>();
    public OrderDbContext(DbContextOptions opts) : base(opts)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasPostgresExtension("unaccent");
    }
}