using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Domain.AggregatesModel;

public class OrderPromotion(
     string name
    , string slug
    , DateTimeOffset startDate
    , DateTimeOffset endDate
    , Guid merchantId
) : AuditedAggregateRoot
{
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = slug;
    public DateTimeOffset StartDate { get; private set; } = startDate;
    public DateTimeOffset EndDate { get; private set; } = endDate;
    public Guid MerchantId { get; private set; } = merchantId;
    public virtual Merchant Merchant { get; private set; } = null!;
    public PromotionStatus Status { get; private set; } = PromotionStatus.NEW;
    public OrderPromotionType Type { get; private set; } = OrderPromotionType.UNSPECIFIED;
    /// <summary>
    /// Depending on the promotion type <br/>
    /// If type is BUNDLE, maximum quantity of the bundle that one customer can buy <br/>
    /// If type is ADD_ON, maximum quantity of the add_on products per order per main product <br/>
    /// If type is GIFT, maximum quantity of the gift customer can choose <br/>
    /// </summary>
    public int MaxQuantity { get; private set; }
    /// <summary>
    /// For GIFT promotion type
    /// </summary>
    public decimal MinSpend { get; private set; }
    /// <summary>
    /// For BUNDLE promotion type
    /// </summary>
    public BundlePromotionDiscountType BundlePromotionDiscountType { get; private set; } = BundlePromotionDiscountType.UNSPECIFIED;
    public readonly List<OrderPromotionCondition> _conditions = [];
    public IReadOnlyCollection<OrderPromotionCondition> Conditions => _conditions;
    public readonly List<OrderPromotionItem> _items = [];
    public virtual IReadOnlyCollection<OrderPromotionItem> Items => _items;
    public readonly List<OrderPromotionSubItem> _subItems = [];
    public virtual IReadOnlyCollection<OrderPromotionSubItem> SubItems => _subItems;
}