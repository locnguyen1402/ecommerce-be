using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Discount(string name, string code, DiscountType type) : EntityWithDiscounts
{
    public string Name { get; private set; } = name;
    public string Code { get; private set; } = code;
    public string? Description { get; private set; }
    public DiscountType Type { get; private set; } = type;
    public decimal? DiscountPercentage { get; private set; }
    public decimal? DiscountAmount { get; set; }
    public decimal? MaxDiscountAmount { get; private set; }
    public DateTimeOffset? StartDate { get; private set; }
    public DateTimeOffset? EndDate { get; private set; }
    public int? MaxRedemptions { get; private set; }
    public int? RedemptionQuantity { get; private set; }
    public bool IsActive { get; private set; }
    public int? LimitationTimes { get; private set; }
    public DiscountLimitationType? LimitationType { get; private set; }

    public readonly List<DiscountUsageHistory> _discountUsageHistory = [];
    public IReadOnlyCollection<DiscountUsageHistory> DiscountUsageHistory => _discountUsageHistory;

    public readonly List<Product> _appliedToProducts = [];
    public IReadOnlyCollection<Product> AppliedToProducts => _appliedToProducts;

    public readonly List<Category> _appliedToCategories = [];
    public IReadOnlyCollection<Category> AppliedToCategories => _appliedToCategories;

    public void Update(string? description
        , decimal? discountPercentage
        , decimal? discountAmount
        , decimal? maxDiscountAmount
        , DateTimeOffset? startDate
        , DateTimeOffset? endDate)
    {
        Description = description;
        DiscountPercentage = discountPercentage;
        DiscountAmount = discountAmount;
        MaxDiscountAmount = maxDiscountAmount;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void SetDiscountType(DiscountType type)
    {
        Type = type;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void IncreaseRedemptionQuantity()
    {
        if (RedemptionQuantity < MaxRedemptions)
            RedemptionQuantity++;
    }

    public void DecreaseRedemptionQuantity()
    {
        if (RedemptionQuantity > 0)
            RedemptionQuantity--;
    }

    public void AddDiscountUsageHistory(Guid orderId, string orderNumber, DiscountUsageHistoryStatus historyStatus)
    {
        var discountHistory = new DiscountUsageHistory(Id, orderId, orderNumber, historyStatus);
        _discountUsageHistory.Add(discountHistory);
    }

    public void AddOrUpdateAppliedToCategories(List<Category> categories)
    {
        if (!HasDiscountsApplied)
            HasDiscountsApplied = true;

        _appliedToCategories.Clear();
        _appliedToCategories.AddRange(categories);
    }

    public void AddOrUpdateAppliedToProducts(List<Product> products)
    {
        if (!HasDiscountsApplied)
            HasDiscountsApplied = true;

        _appliedToProducts.Clear();
        _appliedToProducts.AddRange(products);
    }
}