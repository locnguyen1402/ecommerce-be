using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ProductVariantAttributeValue(string value) : Entity()
{
    public Guid ProductVariantId { get; set; }
    public ProductVariant? ProductVariant { get; set; }
    public Guid ProductAttributeId { get; set; }
    public ProductAttribute? ProductAttribute { get; set; }
    public string Value { get; private set; } = value;

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (ProductVariantAttributeValue)obj;
        return ProductAttributeId == other.ProductAttributeId && Value == other.Value;
    }

    public override int GetHashCode()
        => HashCode.Combine(ProductAttributeId, Value);
}