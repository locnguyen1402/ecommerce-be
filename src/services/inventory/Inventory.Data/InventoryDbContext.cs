using Microsoft.EntityFrameworkCore;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Data.EntityConfigurations;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Data;

public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : BaseDbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
    }
}
