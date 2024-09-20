using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ProductPromotionItem(Guid productPromotionId, Guid productId)
{
    public Guid ProductPromotionId { get; private set; } = productPromotionId;
    public virtual ProductPromotion ProductPromotion { get; private set; } = null!;
    public Guid ProductId { get; private set; } = productId;
    public virtual Product Product { get; private set; } = null!;
    public Guid? ProductVariantId { get; private set; }
    public virtual ProductVariant? ProductVariant { get; private set; }
    public bool IsActive { get; private set; } = false;
    public decimal ListPrice { get; private set; }
    public decimal DiscountPrice { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public int Quantity { get; private set; }
    public NoProductsPerOrderLimit NoProductsPerOrderLimit { get; private set; } = NoProductsPerOrderLimit.UNLIMITED;
    public int MaxItemsPerOrder { get; private set; } = 0;
    public ProductPromotionItem(Guid productPromotionId, Guid productId, Guid? productVariantId) : this(productPromotionId, productId)
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