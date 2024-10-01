using ECommerce.Data.EntityConfigurations;
using ECommerce.Data.EntityConfigurations.IdentityEntities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data;

public static class ModelBuilderExtensions
{
    public static void ConfigureECommerceEntitiesExtensions(this ModelBuilder modelBuilder)
    {
        #region Product & Category
        modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductAttributeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductVariantEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductVariantAttributeValueEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AttributeValueEntityConfiguration());
        #endregion Product & Category

        #region Discount
        modelBuilder.ApplyConfiguration(new DiscountEntityConfiguration());
        #endregion Discount

        #region Merchant & Store
        modelBuilder.ApplyConfiguration(new MerchantEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MerchantCategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StoreEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ShopCollectionEntityConfiguration());
        #endregion Merchant & Store

        #region Order
        modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderContactEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusTrackingEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentMethodTrackingEntityConfiguration());
        #endregion Order

        #region Voucher
        modelBuilder.ApplyConfiguration(new VoucherEntityConfiguration());
        #endregion Voucher

        #region Promotion
        modelBuilder.ApplyConfiguration(new OrderPromotionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderPromotionItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderPromotionSubItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductPromotionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductPromotionItemEntityConfiguration());
        #endregion Promotion

        #region Customer
        modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ContactEntityConfiguration());
        #endregion Customer

        #region ObjectStorage
        modelBuilder.ApplyConfiguration(new ObjectStorageEntityConfiguration());
        #endregion ObjectStorage

        #region ImportHistory
        modelBuilder.ApplyConfiguration(new ImportHistoryEntityConfiguration());
        #endregion ImportHistory

        #region Location
        modelBuilder.ApplyConfiguration(new ProvinceEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DistrictEntityConfiguration());
        modelBuilder.ApplyConfiguration(new WardEntityConfiguration());
        #endregion Location
    }

    public static void ConfigureIdentityEntitiesExtensions(this ModelBuilder modelBuilder)
    {
        #region OpnIddict
        modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorizationConfiguration());
        modelBuilder.ApplyConfiguration(new ScopeConfiguration());
        modelBuilder.ApplyConfiguration(new TokenConfiguration());
        modelBuilder.ApplyConfiguration(new ClientRoleConfiguration());
        #endregion OpnIddict

        #region Identity
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
        modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
        #endregion Identity

        #region Security
        modelBuilder.ApplyConfiguration(new PermissionGroupConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new SecurityEventConfiguration());
        #endregion Security
    }
}