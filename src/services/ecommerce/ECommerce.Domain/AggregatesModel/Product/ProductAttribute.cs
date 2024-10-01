using ECommerce.Shared.Common.AggregatesModel.Auditing;

namespace ECommerce.Domain.AggregatesModel;

public class ProductAttribute(string name) : AuditedAggregateRoot
{
    public string Name { get; private set; } = name.ToLower();
    public bool Predefined { get; private set; } = false;
    public bool IsActive { get; private set; } = true;
    private readonly List<ProductVariantAttributeValue> _productVariantAttributeValues = [];
    public IReadOnlyCollection<ProductVariantAttributeValue> ProductVariantAttributeValues => _productVariantAttributeValues;
    private readonly List<Product> _products = [];
    public IReadOnlyCollection<Product> Products => _products;
    private readonly List<ProductProductAttribute> _productProductAttributes = [];
    public IReadOnlyCollection<ProductProductAttribute> ProductProductAttributes => _productProductAttributes;

    public readonly List<AttributeValue> _attributeValues = [];
    public IReadOnlyCollection<AttributeValue> AttributeValues => _attributeValues;

    public void UpdateName(string name)
    {
        Name = name.ToLower();
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void SetPredefined()
    {
        Predefined = true;
    }
}
