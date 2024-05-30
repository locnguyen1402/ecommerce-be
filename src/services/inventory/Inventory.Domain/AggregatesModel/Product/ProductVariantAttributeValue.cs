using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ProductVariantAttributeValue(string value) : Entity()
{
    public Guid ProductVariantId { get; private set; }
    public ProductVariant ProductVariant { get; private set; } = null!;
    public Guid ProductAttributeId { get; private set; }
    public ProductAttribute ProductAttribute { get; private set; } = null!;
    public string Value { get; private set; } = value;
}