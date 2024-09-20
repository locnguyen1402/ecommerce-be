using Microsoft.EntityFrameworkCore;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Data.EntityConfigurations;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Data;

public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : BaseDbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
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

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<OrderStatusTracking> OrderStatusTrackings => Set<OrderStatusTracking>();
    public DbSet<PaymentMethodTracking> PaymentMethodTrackings => Set<PaymentMethodTracking>();
    public DbSet<Voucher> Vouchers => Set<Voucher>();

    public DbSet<OrderPromotion> OrderPromotions => Set<OrderPromotion>();
    public DbSet<OrderPromotionItem> OrderPromotionItems => Set<OrderPromotionItem>();
    public DbSet<OrderContact> OrderContacts => Set<OrderContact>();
    public DbSet<OrderPromotionSubItem> OrderPromotionSubItems => Set<OrderPromotionSubItem>();
    public DbSet<ProductPromotion> ProductPromotions => Set<ProductPromotion>();
    public DbSet<ProductPromotionItem> ProductPromotionItems => Set<ProductPromotionItem>();

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Contact> Contacts => Set<Contact>();

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

        // Order
        modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderContactEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusTrackingEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentMethodTrackingEntityConfiguration());

        // Voucher
        modelBuilder.ApplyConfiguration(new VoucherEntityConfiguration());

        // Promotion
        modelBuilder.ApplyConfiguration(new OrderPromotionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderPromotionItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderPromotionSubItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductPromotionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductPromotionItemEntityConfiguration());

        // Customer
        modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ContactEntityConfiguration());
    }
}
