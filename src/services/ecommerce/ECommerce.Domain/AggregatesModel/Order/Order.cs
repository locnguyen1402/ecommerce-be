using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Domain.AggregatesModel;

public class Order(Guid customerId, string phoneNumber) : AuditedAggregateRoot
{
    public string OrderNumber { get; private set; } = string.Empty;
    public Guid CustomerId { get; private set; } = customerId;
    public virtual Customer Customer { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = phoneNumber;
    public OrderStatus Status { get; private set; } = OrderStatus.TO_PAY;
    public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.UNPAID;
    public DateTimeOffset PaidAt { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; } = PaymentMethod.UNSPECIFIED;

    /// <summary>
    /// Total price of all items in the order before vat price
    /// </summary>
    public decimal TotalItemPrice { get; private set; } = 0;
    public decimal VatPercent { get; private set; } = 0;
    public decimal VatPrice { get => TotalItemPrice * VatPercent / 100; private set { } }
    // TODO: Integrate promotion and voucher
    // public decimal TotalDiscountPrice => 0;
    public decimal DeliveryFee { get; private set; } = 0;
    public decimal TotalPrice { get; private set; } = 0;
    public DateTimeOffset DeliverySchedule { get; private set; }
    public OrderContact OrderContact { get; private set; } = null!;
    public Guid MerchantId { get; private set; }
    public virtual Merchant Merchant { get; private set; } = null!;
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

    public void SetOrderContact(OrderContact orderContact)
    {
        OrderContact = orderContact;
    }

    public void SetDeliveryFee(decimal deliveryFee)
    {
        DeliveryFee = deliveryFee;
    }

    public void SetMerchant(Guid merchantId)
    {
        MerchantId = merchantId;
    }

    public void SetOrderNumber(string orderNumber)
    {
        OrderNumber = orderNumber;
    }

    public void AddOrderStatus(OrderStatus orderStatus)
    {
        Status = orderStatus;
        var orderStatusTracking = new OrderStatusTracking(Id, orderStatus);

        _orderStatusTrackings.Add(orderStatusTracking);
    }

    public void AddOrderItems(List<OrderItem> orderItems)
    {
        orderItems.ForEach(item => _orderItems.Add(item));

        RecalculateTotalPrice();
    }

    public void RecalculateTotalPrice()
    {
        TotalItemPrice = _orderItems
            .Aggregate((decimal)0, (total, item) => total + item.TotalPrice);

        TotalPrice = TotalItemPrice + DeliveryFee;
    }

    public void SetPaymentMethod(PaymentMethod paymentMethod)
    {
        PaymentMethod = paymentMethod;
    }
}