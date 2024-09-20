using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ProductPromotionItem(Guid productId) : Entity
{
    public Guid ProductPromotionId { get; private set; }
    public virtual ProductPromotion ProductPromotion { get; private set; } = null!;
    public Guid ProductId { get; private set; } = productId;
    public virtual Product Product { get; private set; } = null!;
    public Guid? ProductVariantId { get; private set; }
    public virtual ProductVariant? ProductVariant { get; private set; }
    public bool IsActive { get; private set; } = false;
    public decimal ListPrice { get; private set; } = 0;
    public decimal DiscountPrice { get; private set; } = 0;
    public decimal DiscountPercentage { get; private set; } = 0;
    public int Quantity { get; private set; }
    public NoProductsPerOrderLimit NoProductsPerOrderLimit { get; private set; } = NoProductsPerOrderLimit.UNLIMITED;
    public int MaxItemsPerOrder { get; private set; } = 0;
    public ProductPromotionItem(Guid productId, Guid? productVariantId) : this(productId)
    {
        ProductVariantId = productVariantId;
    }
    public void Activate()
    {
        IsActive = true;
    }
    public void Deactivate()
    {
        IsActive = false;
    }
    public void SetDiscount(
        decimal listPrice,
        decimal discountPrice,
        decimal discountPercentage
    )
    {
        // TODO: Validate discount price and percentage
        ListPrice = listPrice;
        DiscountPrice = discountPrice;
        DiscountPercentage = discountPercentage;
    }
    public void SetQuantity(int quantity)
    {
        Quantity = quantity;
    }
    public void SetNoProductsPerOrderLimit(NoProductsPerOrderLimit noProductsPerOrderLimit, int maxItemsPerOrder = 0)
    {
        NoProductsPerOrderLimit = noProductsPerOrderLimit;
        MaxItemsPerOrder = maxItemsPerOrder;
    }
}