using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class OrderPromotionSubItem(Guid promotionId, Guid productId)
{
    public Guid OrderPromotionId { get; private set; } = promotionId;
    public virtual OrderPromotion OrderPromotion { get; private set; } = null!;
    public Guid ProductId { get; private set; } = productId;
    public virtual Product Product { get; private set; } = null!;
    public Guid? ProductVariantId { get; private set; }
    public virtual ProductVariant? ProductVariant { get; private set; }
    public bool IsActive { get; private set; } = false;
    public OrderPromotionSubItemType Type { get; private set; } = OrderPromotionSubItemType.UNSPECIFIED;
    public decimal DiscountPrice { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public NoProductsPerOrderLimit NoProductsPerOrderLimit { get; private set; } = NoProductsPerOrderLimit.SPECIFIED;
    public int MaxItemsPerOrder { get; private set; } = 1;
    public void Activate()
    {
        IsActive = true;
    }
    public void Deactivate()
    {
        IsActive = false;
    }
}