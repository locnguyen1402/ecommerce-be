using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class AttributeValue(string value) : Entity()
{
    public string Value { get; private set; } = value;
    public bool Predefined { get; private set; } = false;

    public Guid? ProductAttributeId { get; private set; }
    public ProductAttribute? ProductAttribute { get; private set; }

    public readonly List<ProductVariantAttributeValue> _productVariantAttributeValues = [];
    public IReadOnlyCollection<ProductVariantAttributeValue> ProductVariantAttributeValues => _productVariantAttributeValues;

    public void UpdateValue(string value)
    {
        Value = value;
    }

    public void SetAttribute(Guid attributeId)
    {
        ProductAttributeId = attributeId;
    }

    public void SetPredefined()
    {
        Predefined = true;
    }
}
