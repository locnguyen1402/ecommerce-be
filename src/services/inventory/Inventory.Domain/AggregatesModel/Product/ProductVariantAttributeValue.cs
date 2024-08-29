using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ProductVariantAttributeValue : Entity
{
    public Guid ProductVariantId { get; set; }
    public ProductVariant? ProductVariant { get; set; }
    public Guid ProductAttributeId { get; set; }
    public ProductAttribute? ProductAttribute { get; set; }
    public Guid AttributeValueId { get; private set; }
    public virtual AttributeValue AttributeValue { get; private set; } = null!;
    public ProductVariantAttributeValue(string value, Guid? attributeValueId)
    {
        if (value.Trim().Length == 0)
        {
            throw new Exception("Attribute value cannot be empty");
        }

        AddOrUpdateAttributeValue(value, attributeValueId);
    }

    public ProductVariantAttributeValue(Guid attributeId, string value, Guid? attributeValueId) : this(value, attributeValueId)
    {
        ProductAttributeId = attributeId;
        AddOrUpdateAttributeValue(value, attributeValueId);
    }
    public ProductVariantAttributeValue(ProductAttribute attribute, string value, Guid? attributeValueId) : this(value, attributeValueId)
    {
        ProductAttribute = attribute;
        AddOrUpdateAttributeValue(value, attributeValueId);
    }

    public void UpdateValue(string value, Guid? attributeValueId)
    {
        if (value.Trim().Length == 0)
        {
            throw new Exception("Attribute value cannot be empty");
        }

        AddOrUpdateAttributeValue(value, attributeValueId);
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
            && AttributeValueId == other.AttributeValueId;
    }

    public override int GetHashCode()
        => HashCode.Combine(ProductAttributeId, AttributeValueId);

    public void AddOrUpdateAttributeValue(string value, Guid? attributeValueId = null)
    {
        if (attributeValueId == null || attributeValueId == Guid.Empty)
        {
            AddAttributeValue(value);
        }
        else
        {
            AttributeValueId = attributeValueId.Value;
            AttributeValue.UpdateValue(value);
        }
    }


    public void AddAttributeValue(string value)
    {
        var attributeValue = new AttributeValue(value);
        attributeValue.SetAttribute(ProductAttributeId);
        AttributeValue = attributeValue;
    }
}