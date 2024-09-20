using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class PaymentMethodTracking(Guid orderId, PaymentMethod paymentMethod, decimal value) : Entity
{
    // TODO: Save additional payment method information depending on specific payment method in value object 
    public PaymentMethod PaymentMethod { get; private set; } = paymentMethod;
    public decimal Value { get; private set; } = value;
    public Guid OrderId { get; private set; } = orderId;
    public virtual Order Order { get; private set; } = null!;
}