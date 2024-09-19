using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class PromotionAddOnDeal : Entity
{
    public Guid PromotionId { get; private set; }
    public PromotionAddOnDealType AddOnDealType { get; private set; }
    public decimal? MinSpend { get; private set; }
    public int? MaxGift { get; private set; }
    public virtual Promotion Promotion { get; private set; } = null!;

    public readonly HashSet<AddOnDealProduct> _addOnDealProducts = [];
    public IReadOnlyCollection<AddOnDealProduct> AddOnDealProducts => _addOnDealProducts;

    public readonly HashSet<AddOnDealGift> _addOnDealGifts = [];
    public IReadOnlyCollection<AddOnDealGift> AddOnDealGifts => _addOnDealGifts;
}

public class AddOnDealProduct : Entity
{
    public Guid ProductId { get; private set; }
    public virtual Product Product { get; private set; } = null!;
    public bool IsActive { get; private set; } = true;
    public Guid PromotionAddOnDealId { get; private set; }
    public virtual PromotionAddOnDeal PromotionAddOnDeal { get; private set; } = null!;
}

public class AddOnDealGift : Entity
{
    public Guid ProductId { get; private set; }
    public virtual Product Product { get; private set; } = null!;

    public Guid PromotionAddOnDealId { get; private set; }
    public virtual PromotionAddOnDeal PromotionAddOnDeal { get; private set; } = null!;
}