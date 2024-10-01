using ECommerce.Shared.Common.Enums;

namespace ECommerce.Domain.AggregatesModel;

public class Discount(
    string name
    , string slug
    , string code) : EntityWithDiscounts
{
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = slug;
    public string Code { get; private set; } = code;
    public string? Description { get; private set; }
    public DiscountType DiscountType { get; private set; } = DiscountType.UNSPECIFIED;
    public decimal? DiscountValue { get; private set; }
    public DiscountUnit DiscountUnit { get; private set; } = DiscountUnit.UNSPECIFIED;
    public decimal? MinOrderValue { get; private set; }
    public decimal? MaxDiscountAmount { get; private set; }
    public DateTimeOffset? StartDate { get; private set; }
    public DateTimeOffset? EndDate { get; private set; }
    public bool IsActive { get; private set; } = true;
    public int? LimitationTimes { get; private set; }
    public DiscountLimitationType? LimitationType { get; private set; }

    public readonly List<DiscountUsageHistory> _discountUsageHistory = [];
    public IReadOnlyCollection<DiscountUsageHistory> DiscountUsageHistory => _discountUsageHistory;

    public readonly List<Product> _appliedToProducts = [];
    public IReadOnlyCollection<Product> AppliedToProducts => _appliedToProducts;

    public readonly List<Category> _appliedToCategories = [];
    public IReadOnlyCollection<Category> AppliedToCategories => _appliedToCategories;

    public void Update(
        string? description
        , decimal? discountValue
        , decimal? minOrderValue
        , decimal? maxDiscountAmount
        , DateTimeOffset? startDate
        , DateTimeOffset? endDate)
    {
        Description = description;
        DiscountValue = discountValue;
        MinOrderValue = minOrderValue;
        MaxDiscountAmount = maxDiscountAmount;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void SetDiscountType(DiscountType type)
    {
        DiscountType = type;
    }

    public void SetDiscountUnit(DiscountUnit unit)
    {
        DiscountUnit = unit;
    }

    public void SetLimitation(int? times, DiscountLimitationType? type)
    {
        LimitationTimes = times;
        LimitationType = type;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
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