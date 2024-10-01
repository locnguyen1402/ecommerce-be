using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Domain.AggregatesModel;

public class PaymentMethodTracking(Guid orderId, PaymentMethod paymentMethod, decimal value) : AuditedAggregateRoot
{
    // TODO: Save additional payment method information depending on specific payment method in value object 
    public PaymentMethod PaymentMethod { get; private set; } = paymentMethod;
    public decimal Value { get; private set; } = value;
    public Guid OrderId { get; private set; } = orderId;
    public virtual Order Order { get; private set; } = null!;
}