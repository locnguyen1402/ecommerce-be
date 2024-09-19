using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Voucher(string code, VoucherType type) : Entity
{
    public string Code { get; private set; } = code;
    public string Name { get; private set; } = string.Empty;
    public VoucherType Type { get; private set; } = type;
    public DateTimeOffset StartDate { get; private set; }
    public DateTimeOffset EndDate { get; private set; }
    public VoucherApplyType ApplyType { get; private set; } = VoucherApplyType.UNSPECIFIED;
    public DiscountUnit DiscountUnit { get; private set; } = DiscountUnit.UNSPECIFIED;
    public decimal? DiscountValue { get; private set; }
    public decimal? MinSpend { get; private set; }
    public int? MaxQuantity { get; private set; }
    public int? MaxQuantityPerUser { get; private set; }
    public VoucherDisplayType DisplayType { get; private set; } = VoucherDisplayType.UNSPECIFIED;

    public readonly HashSet<VoucherProduct> _voucherProducts = [];
    public IReadOnlyCollection<VoucherProduct> VoucherProducts => _voucherProducts;
}