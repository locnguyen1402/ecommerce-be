using Microsoft.EntityFrameworkCore;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Data.EntityConfigurations;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Data;

public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : BaseDbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<CategoryProduct> CategoryProducts => Set<CategoryProduct>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductAttribute> ProductAttributes => Set<ProductAttribute>();
    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
    public DbSet<ProductVariantAttributeValue> ProductVariantAttributeValues => Set<ProductVariantAttributeValue>();
    public DbSet<ProductProductAttribute> ProductProductAttributes => Set<ProductProductAttribute>();
    public DbSet<AttributeValue> AttributeValues => Set<AttributeValue>();
    public DbSet<Discount> Discounts => Set<Discount>();
    public DbSet<Merchant> Merchants => Set<Merchant>();
    public DbSet<MerchantCategory> MerchantCategories => Set<MerchantCategory>();
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<ShopCollection> ShopCollections => Set<ShopCollection>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Product & Category
        modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductAttributeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductVariantEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductVariantAttributeValueEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AttributeValueEntityConfiguration());

        // Discount
        modelBuilder.ApplyConfiguration(new DiscountEntityConfiguration());

        // Merchant & Store
        modelBuilder.ApplyConfiguration(new MerchantEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MerchantCategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StoreEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ShopCollectionEntityConfiguration());
    }
}
