using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Libs.Domain;

namespace ECommerce.Domain.AggregatesModel;

public class DiscountUsageHistory(Guid couponId, Guid orderId, string orderNumber, DiscountUsageHistoryStatus status) : ValueObject
{
    public Guid CouponId { get; private set; } = couponId;
    public Guid OrderId { get; private set; } = orderId;
    public string OrderNumber { get; private set; } = orderNumber;
    public DiscountUsageHistoryStatus Status { get; private set; } = status;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CouponId;
        yield return OrderId;
        yield return OrderNumber;
        yield return Status;
    }
}
