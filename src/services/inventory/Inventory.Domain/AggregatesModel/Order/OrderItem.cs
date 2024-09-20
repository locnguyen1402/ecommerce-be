using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class OrderItem(
    Guid? productId,
    Guid? productVariantId,
    decimal unitPrice,
    int quantity
) : Entity
{
    public Guid OrderId { get; private set; }
    public virtual Order Order { get; private set; } = null!;
    public Guid? ProductId { get; private set; } = productId;
    public virtual Product? Product { get; private set; }
    public Guid? ProductVariantId { get; private set; } = productVariantId;
    public virtual ProductVariant? ProductVariant { get; private set; }
    public int Quantity { get; private set; } = quantity;
    public string ProductName { get; private set; } = string.Empty;
    /// <summary>
    /// List of selected variant attribute values
    /// </summary>
    public string ProductDescription { get; private set; } = string.Empty;
    public decimal ListPrice { get; private set; } = 0;
    public decimal UnitPrice { get; private set; } = unitPrice;
    public decimal TotalPrice { get => UnitPrice * Quantity; private set { } }
    public decimal VatPercent { get; private set; } = 0;
    public decimal VatPrice { get => UnitPrice * VatPercent / 100; private set { } }
    public decimal TotalVatPrice { get => VatPrice * Quantity; private set { } }
}