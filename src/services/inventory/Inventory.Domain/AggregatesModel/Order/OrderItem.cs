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
    public Guid? ProductId { get; private set; } = productId;
    public Guid? ProductVariantId { get; private set; } = productVariantId;
    public int Quantity { get; private set; } = quantity;
    public decimal UnitPrice { get; private set; } = unitPrice;
    public decimal TotalPrice { get; private set; } = unitPrice * (decimal)quantity;

    public decimal? VatPrice { get; private set; }
    public decimal? ExceptVatPrice { get; private set; }
    public decimal? TotalVatPrice { get; private set; }
    public decimal? TotalExceptVatPrice { get; private set; }
    public float? VatPercent { get; private set; }

    public virtual Order Order { get; private set; } = null!;
    public virtual Product? Product { get; private set; }
    public virtual ProductVariant? ProductVariant { get; private set; }
}