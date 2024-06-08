using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ProductVariantAttributeValue : Entity
{
    public Guid ProductVariantId { get; set; }
    public ProductVariant? ProductVariant { get; set; }
    public Guid ProductAttributeId { get; set; }
    public ProductAttribute? ProductAttribute { get; set; }
    public string Value { get; private set; }
    public ProductVariantAttributeValue(string value)
    {
        if (value.Trim().Length == 0)
        {
            throw new Exception("Attribute value cannot be empty");
        }

        Value = value;
    }
    public ProductVariantAttributeValue(Guid attributeId, string value) : this(value)
    {
        ProductAttributeId = attributeId;
        Value = value;
    }
    public void UpdateValue(string value)
    {
        if (value.Trim().Length == 0)
        {
            throw new Exception("Attribute value cannot be empty");
        }

        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (ProductVariantAttributeValue)obj;
        return ProductVariantId == other.ProductVariantId
            && ProductAttributeId == other.ProductAttributeId
            && Value == other.Value;
    }

    public override int GetHashCode()
        => HashCode.Combine(ProductAttributeId, Value);
}