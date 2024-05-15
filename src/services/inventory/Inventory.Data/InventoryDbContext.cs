using Microsoft.EntityFrameworkCore;

using ECommerce.Inventory.Domain.AggregatesModel.Product;

namespace ECommerce.Inventory.Data;

public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
