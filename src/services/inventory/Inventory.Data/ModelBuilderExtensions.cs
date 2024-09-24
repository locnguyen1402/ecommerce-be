using ECommerce.Inventory.Data.EntityConfigurations;
using ECommerce.Inventory.Data.EntityConfigurations.IdentityEntities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Data;

public static class ModelBuilderExtensions
{
    public static void ConfigureInventoryEntitiesExtensions(this ModelBuilder modelBuilder)
    {
        #region Product & Category
        modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductAttributeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductVariantEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductVariantAttributeValueEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AttributeValueEntityConfiguration());
        #endregion

        #region Discount
        modelBuilder.ApplyConfiguration(new DiscountEntityConfiguration());
        #endregion

        #region Merchant & Store
        modelBuilder.ApplyConfiguration(new MerchantEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MerchantCategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StoreEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ShopCollectionEntityConfiguration());
        #endregion

        #region Order
        modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderContactEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusTrackingEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentMethodTrackingEntityConfiguration());
        #endregion

        #region Voucher
        modelBuilder.ApplyConfiguration(new VoucherEntityConfiguration());
        #endregion

        #region Promotion
        modelBuilder.ApplyConfiguration(new OrderPromotionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderPromotionItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderPromotionSubItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductPromotionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductPromotionItemEntityConfiguration());
        #endregion

        #region Customer
        modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ContactEntityConfiguration());
        #endregion
    }

    public static void ConfigureIdentityEntitiesExtensions(this ModelBuilder modelBuilder)
    {
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