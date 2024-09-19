using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class PaymentMethodTracking(Guid orderId, PaymentMethod paymentMethod) : Entity
{
    public Guid OrderId { get; private set; } = orderId;
    public PaymentMethod PaymentMethod { get; private set; } = paymentMethod;
    public string? RefId { get; private set; }
    public string? RefCode { get; private set; }
    public string? RefName { get; private set; }
    public decimal? Value { get; private set; }
    public decimal? BaseValue { get; private set; }
    public virtual Order Order { get; private set; } = null!;
}