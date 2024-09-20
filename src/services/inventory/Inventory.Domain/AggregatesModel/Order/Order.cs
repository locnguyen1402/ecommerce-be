using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Order(Guid customerId) : Entity
{
    public string OrderNumber { get; private set; } = string.Empty;
    public Guid CustomerId { get; private set; } = customerId;
    public virtual Customer Customer { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = string.Empty;
    public OrderStatus Status { get; private set; } = OrderStatus.TO_PAY;
    public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.UNPAID;
    public DateTimeOffset PaidAt { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; } = PaymentMethod.UNSPECIFIED;

    /// <summary>
    /// Total price of all items in the order before vat price
    /// </summary>
    public decimal TotalItemPrice { get; private set; } = 0;
    public decimal VatPercent { get; private set; } = 0;
    public decimal VatPrice => TotalItemPrice * VatPercent / 100;
    // TODO: Integrate promotion and voucher
    // public decimal TotalDiscountPrice => 0;
    public decimal DeliveryFee { get; private set; } = 0;
    public decimal TotalPrice { get; private set; } = 0;
    public DateTimeOffset DeliverySchedule { get; private set; }
    public string DeliveryAddress { get; private set; } = string.Empty;
    public Guid StoreId { get; private set; }
    public virtual Store Store { get; private set; } = null!;
    private readonly HashSet<OrderItem> _orderItems = [];
    public virtual IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    private readonly HashSet<OrderStatusTracking> _orderStatusTrackings = [];
    public virtual IReadOnlyCollection<OrderStatusTracking> OrderStatusTrackings => _orderStatusTrackings;
    private readonly HashSet<PaymentMethodTracking> _paymentMethodTrackings = [];
    public virtual IReadOnlyCollection<PaymentMethodTracking> PaymentMethodTrackings => _paymentMethodTrackings;
    public string? Notes { get; private set; }

    public void ConfirmPaid()
    {
        if (PaymentStatus == PaymentStatus.PAID)
        {
            throw new InvalidOperationException("Order is already paid");
        }

        PaymentStatus = PaymentStatus.PAID;
        PaidAt = DateTimeOffset.UtcNow;
    }
}