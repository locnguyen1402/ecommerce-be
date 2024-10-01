using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Domain.AggregatesModel;

public class ProductPromotion(
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
    public ProductPromotionType Type { get; private set; } = ProductPromotionType.UNSPECIFIED;
    public readonly List<ProductPromotionItem> _items = [];
    public virtual IReadOnlyCollection<ProductPromotionItem> Items => _items;
    public void SetNormalPromotion()
    {
        Type = ProductPromotionType.NORMAL;
    }
    public void SetFlashSalePromotion()
    {
        Type = ProductPromotionType.FLASH_SALE;
    }
    public void SetItems(List<ProductPromotionItem> items)
    {
        _items.Clear();
        _items.AddRange(items);
    }
}