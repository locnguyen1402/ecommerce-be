using ECommerce.Shared.Common.AggregatesModel.Auditing;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class OrderPromotionItem(Guid orderPromotionId, Guid productId) : AuditedAggregateRoot
{
    public Guid OrderPromotionId { get; private set; } = orderPromotionId;
    public virtual OrderPromotion OrderPromotion { get; private set; } = null!;
    public Guid ProductId { get; private set; } = productId;
    public virtual Product Product { get; private set; } = null!;
    public bool IsActive { get; private set; } = true;

    public void Activate()
    {
        IsActive = true;
    }
    public void Deactivate()
    {
        IsActive = false;
    }
}