using ECommerce.Shared.Common.AggregatesModel.Auditing;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ProductVariantAttributeValue : AuditedAggregateRoot
{
    public Guid ProductVariantId { get; set; }
    public ProductVariant? ProductVariant { get; set; }
    public Guid ProductAttributeId { get; set; }
    public ProductAttribute? ProductAttribute { get; set; }
    public string Value { get; private set; }
    public Guid? AttributeValueId { get; private set; }
    public virtual AttributeValue? AttributeValue { get; private set; }

    public ProductVariantAttributeValue(string value, Guid? attributeValueId)
    {
        if (value.Trim().Length == 0)
        {
            throw new Exception("Attribute value cannot be empty");
        }

        Value = value;

        if (attributeValueId != null && attributeValueId != Guid.Empty)
        {
            AttributeValueId = attributeValueId;
        }
    }

    public ProductVariantAttributeValue(Guid attributeId, string value, Guid? attributeValueId) : this(value, attributeValueId)
    {
        ProductAttributeId = attributeId;
    }

    public ProductVariantAttributeValue(ProductAttribute attribute, string value, Guid? attributeValueId) : this(value, attributeValueId)
    {
        ProductAttribute = attribute;
    }

    public void UpdateValue(string value, Guid? attributeValueId)
    {
        if (value.Trim().Length == 0)
        {
            throw new Exception("Attribute value cannot be empty");
        }

        Value = value;

        if (attributeValueId != null && attributeValueId != Guid.Empty)
        {
            AttributeValueId = attributeValueId;
        }
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