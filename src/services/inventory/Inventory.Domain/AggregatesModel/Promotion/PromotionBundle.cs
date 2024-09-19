using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class PromotionBundle : Entity
{
    public Guid PromotionId { get; private set; }
    public PromotionBundleType BundleType { get; private set; }
    public int? MaxOrderQuantityPerUser { get; private set; }
    public virtual Promotion Promotion { get; private set; } = null!;
    public readonly HashSet<BundleCondition> _bundleConditions = [];
    public IReadOnlyCollection<BundleCondition> BundleConditions => _bundleConditions;
    public readonly HashSet<BundleProduct> _bundleProducts = [];
    public IReadOnlyCollection<BundleProduct> BundleProducts => _bundleProducts;
}

public class BundleCondition : Entity
{
    public int Quantity { get; private set; }
    // $"Mua {0} sản phẩm để được giảm {1}"; => PERCENTAGE, PRICE
    // $"Mua {0} sản phẩm chỉ với giá {1}"; => SAME_PRICE
    public DiscountUnit DiscountUnit { get; private set; } = DiscountUnit.UNSPECIFIED;
    public decimal? DiscountValue { get; private set; }

    public Guid PromotionBundleId { get; private set; }
    public virtual PromotionBundle PromotionBundle { get; private set; } = null!;
}

public class BundleProduct : Entity
{
    public Guid ProductId { get; private set; }
    public virtual Product Product { get; private set; } = null!;
    public bool IsActive { get; private set; } = true;

    public Guid PromotionBundleId { get; private set; }
    public virtual PromotionBundle PromotionBundle { get; private set; } = null!;
}