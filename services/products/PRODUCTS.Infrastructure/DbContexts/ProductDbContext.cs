namespace ECommerce.Products.Infrastructure.DbContexts;
public class ProductDbContext : BaseDbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public ProductDbContext(DbContextOptions opts) : base(opts)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasPostgresExtension("unaccent");
    }
}