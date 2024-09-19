using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Promotion(
    string name
    , string slug
    , DateTimeOffset startDate
    , DateTimeOffset endDate
    , PromotionStatus promotionStatus
    , Guid merchantId
) : Entity
{
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = slug;
    public DateTimeOffset StartDate { get; private set; } = startDate;
    public DateTimeOffset EndDate { get; private set; } = endDate;
    public PromotionType PromotionType { get; private set; } = PromotionType.UNSPECIFIED;
    public PromotionStatus PromotionStatus { get; private set; } = promotionStatus;
    public Guid MerchantId { get; private set; } = merchantId;
    public virtual Merchant Merchant { get; private set; } = null!;

    public readonly HashSet<PromotionDiscount> _promotionDiscounts = [];
    public IReadOnlyCollection<PromotionDiscount> PromotionDiscounts => _promotionDiscounts;

    public readonly HashSet<PromotionAddOnDeal> _promotionAddOnDeals = [];
    public IReadOnlyCollection<PromotionAddOnDeal> PromotionAddOnDeals => _promotionAddOnDeals;

    public readonly HashSet<PromotionBundle> _promotionBundles = [];
    public IReadOnlyCollection<PromotionBundle> PromotionBundles => _promotionBundles;
}
