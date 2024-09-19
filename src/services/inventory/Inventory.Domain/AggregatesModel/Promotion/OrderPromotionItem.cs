using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class OrderPromotionItem(Guid promotionId, Guid productId)
{
    public Guid OrderPromotionId { get; private set; } = promotionId;
    public virtual OrderPromotion OrderPromotion { get; private set; } = null!;
    public Guid ProductId { get; private set; } = productId;
    public virtual Product Product { get; private set; } = null!;
    public bool IsActive { get; private set; } = false;
    public void Activate()
    {
        IsActive = true;
    }
    public void Deactivate()
    {
        IsActive = false;
    }
}