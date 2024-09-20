using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Order(Guid customerId) : Entity
{
    public string OrderNumber { get; private set; } = string.Empty;
    public Guid CustomerId { get; private set; } = customerId;
    public string PhoneNumber { get; private set; } = string.Empty;
    public OrderStatus Status { get; private set; } = OrderStatus.TO_PAY;
    public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.UNPAID;
    public PaymentMethod PaymentMethod { get; private set; } = PaymentMethod.UNSPECIFIED;

    // = TotalItemPrice - TotalDiscountPrice + DeliveryFee
    public decimal TotalPrice { get; private set; }
    public decimal? TotalDiscountPrice { get; private set; }
    public decimal? TotalItemPrice { get; private set; }
    public decimal? DeliveryFee { get; private set; }
    public decimal? TotalVatPrice { get; private set; }
    public decimal? TotalExceptVatPrice { get; private set; }
    public DateTimeOffset DeliverySchedule { get; private set; }
    public string DeliveryAddress { get; private set; } = string.Empty;
    public Guid StoreId { get; private set; }
    public virtual Customer Customer { get; private set; } = null!;
    public virtual Store Store { get; private set; } = null!;
    private readonly HashSet<OrderItem> _orderItems = [];
    public virtual IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    private readonly HashSet<OrderStatusTracking> _orderStatusTrackings = [];
    public virtual IReadOnlyCollection<OrderStatusTracking> OrderStatusTrackings => _orderStatusTrackings;

    private readonly HashSet<PaymentStatusTracking> _paymentStatusTrackings = [];
    public virtual IReadOnlyCollection<PaymentStatusTracking> PaymentStatusTrackings => _paymentStatusTrackings;

    private readonly HashSet<PaymentMethodTracking> _paymentMethodTrackings = [];
    public virtual IReadOnlyCollection<PaymentMethodTracking> PaymentMethodTrackings => _paymentMethodTrackings;

    public string? Notes { get; private set; }
}