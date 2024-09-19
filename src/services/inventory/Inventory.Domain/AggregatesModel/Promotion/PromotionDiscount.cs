using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class PromotionDiscount : Entity
{
    public Guid PromotionId { get; private set; }
    public virtual Promotion Promotion { get; private set; } = null!;
    public int? MaxOrderQuantity { get; private set; }
    public Guid ProductVariantId { get; private set; }
    public ProductVariant ProductVariant { get; private set; } = null!;
    public double? DiscountPercent { get; private set; }
    public decimal? DiscountPrice { get; private set; }
    public int? DiscountQuantity { get; private set; }
    public bool IsActive { get; private set; } = true;
}
