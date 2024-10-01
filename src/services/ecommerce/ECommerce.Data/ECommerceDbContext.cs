using Microsoft.EntityFrameworkCore;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Data.EntityConfigurations;
using ECommerce.Shared.Common.Infrastructure.Data;
using ECommerce.Shared.Data.Extensions;

namespace ECommerce.Data;

public class ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : BaseDbContext(options)
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

    public DbSet<ObjectStorage> ObjectStorages => Set<ObjectStorage>();

    public DbSet<ImportHistory> ImportHistories => Set<ImportHistory>();

    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<District> Districts => Set<District>();
    public DbSet<Ward> Wards => Set<Ward>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureExtensions();
        modelBuilder.UseCustomDbFunctions();
        modelBuilder.UseCustomPostgreSQLDbFunctions();

        ModelBuilderExtensions.ConfigureECommerceEntitiesExtensions(modelBuilder);
    }
}
