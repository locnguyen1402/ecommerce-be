using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class PaymentStatusTracking(Guid orderId, PaymentStatus paymentStatus) : Entity
{
    public Guid OrderId { get; private set; } = orderId;
    public PaymentStatus PaymentStatus { get; private set; } = paymentStatus;
    public virtual Order Order { get; private set; } = null!;
}