using System.Reflection;
using ECommerce.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Product;

public class ProductDbContext : BaseDbContext
{
    public DbSet<Product> Products => Set<Product>();
    public ProductDbContext(DbContextOptions opts) : base(opts)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasPostgresExtension("unaccent");
    }
}