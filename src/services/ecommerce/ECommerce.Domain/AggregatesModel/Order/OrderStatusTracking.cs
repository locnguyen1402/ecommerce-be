using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Domain.AggregatesModel;

public class OrderStatusTracking(Guid orderId, OrderStatus orderStatus) : AuditedAggregateRoot
{
    public Guid OrderId { get; set; } = orderId;
    public OrderStatus OrderStatus { get; set; } = orderStatus;
    public virtual Order Order { get; set; } = null!;
}