using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Voucher(string name, string code) : AuditedAggregateRoot
{
    public string Name { get; private set; } = name;
    /// <summary>
    /// Code is not unique </br>
    /// There is only one voucher with same code IN_PROGRESS at a time
    /// </summary>
    public string Code { get; private set; } = code;
    public VoucherStatus Status { get; private set; } = VoucherStatus.NEW;
    public DateTimeOffset StartDate { get; private set; }
    public DateTimeOffset EndDate { get; private set; }
    public VoucherAppliedOnType AppliedOnType { get; private set; } = VoucherAppliedOnType.ALL_PRODUCTS;
    public VoucherTargetCustomerType TargetCustomerType { get; private set; } = VoucherTargetCustomerType.ALL;
    public VoucherPopularType PopularType { get; private set; } = VoucherPopularType.PUBLIC;
    public decimal MinSpend { get; private set; } = 0;
    public int MaxQuantity { get; private set; } = 0;
    public int MaxQuantityPerUser { get; private set; } = 1;
    public VoucherType Type { get; private set; } = VoucherType.UNSPECIFIED;
    public VoucherDiscountType DiscountType { get; private set; } = VoucherDiscountType.UNSPECIFIED;
    public decimal Value { get; private set; } = 0;
    public decimal? MaxValue { get; private set; } = 0;
    public Guid MerchantId { get; private set; }
    public virtual Merchant Merchant { get; private set; } = null!;
    public readonly List<Product> _products = [];
    public virtual IReadOnlyCollection<Product> Products => _products;
    public readonly List<VoucherProduct> _voucherProducts = [];
    public virtual IReadOnlyCollection<VoucherProduct> VoucherProducts => _voucherProducts;
    public void SetPeriod(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
    public void SetValidity(int maxQuantity, int maxQuantityPerUser, decimal minSpend = 0)
    {
        MaxQuantity = maxQuantity;
        MaxQuantityPerUser = maxQuantityPerUser;
        MinSpend = minSpend;
    }
    public void ExtendQuantity(int quantity)
    {
        MaxQuantity += quantity;
    }
    public void SetDiscountInfo(VoucherType type, VoucherDiscountType discountType, decimal value, decimal? maxValue)
    {
        Type = type;
        DiscountType = discountType;
        Value = value;
        MaxValue = maxValue;
    }
    public void SetAllProducts()
    {
        AppliedOnType = VoucherAppliedOnType.ALL_PRODUCTS;
    }
    public void SetProducts(List<Product> products)
    {
        AppliedOnType = VoucherAppliedOnType.SPECIFIC_PRODUCTS;

        _products.Clear();
        _products.AddRange(products);
    }
    public void SetTargetCustomerType(VoucherTargetCustomerType targetCustomerType)
    {
        TargetCustomerType = targetCustomerType;
    }
    public void SetPopularType(VoucherPopularType popularType)
    {
        PopularType = popularType;
    }

    public void SetMerchant(Guid merchantId)
    {
        MerchantId = merchantId;
    }
}