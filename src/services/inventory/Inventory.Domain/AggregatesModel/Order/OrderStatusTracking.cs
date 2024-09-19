using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class OrderStatusTracking(Guid orderId, OrderStatus orderStatus) : Entity
{
    public Guid OrderId { get; set; } = orderId;
    public OrderStatus OrderStatus { get; set; } = orderStatus;
}