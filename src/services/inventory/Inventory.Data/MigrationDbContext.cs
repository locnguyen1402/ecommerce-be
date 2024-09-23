using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;

using ECommerce.Shared.Common.Infrastructure.Data;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Data.EntityConfigurations;
using ECommerce.Inventory.Domain.AggregatesModel.Identity;
using ECommerce.Inventory.Data.EntityConfigurations.IdentityEntities;

namespace ECommerce.Inventory.Data;

public class MigrationDbContext(DbContextOptions<MigrationDbContext> options) :
    Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<User, Role, Guid,
            UserClaim, UserRole, UserLogin,
            RoleClaim, UserToken>(options), IDataProtectionKeyContext
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

    public DbSet<DataProtectionKey> DataProtectionKeys => Set<DataProtectionKey>();
    public DbSet<PermissionGroup> PermissionGroups => Set<PermissionGroup>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<SecurityEvent> SecurityEvents => Set<SecurityEvent>();
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<Authorization> Authorizations => Set<Authorization>();
    public DbSet<Scope> Scopes => Set<Scope>();
    public DbSet<Token> Tokens => Set<Token>();
    public DbSet<ClientRole> ClientRoles => Set<ClientRole>();

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

        #region OpnIddict

        modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorizationConfiguration());
        modelBuilder.ApplyConfiguration(new ScopeConfiguration());
        modelBuilder.ApplyConfiguration(new TokenConfiguration());
        modelBuilder.ApplyConfiguration(new ClientRoleConfiguration());

        #endregion

        #region Identity

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
        modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserTokenConfiguration());

        #endregion

        #region Security

        modelBuilder.ApplyConfiguration(new PermissionGroupConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new SecurityEventConfiguration());

        #endregion
    }
}
