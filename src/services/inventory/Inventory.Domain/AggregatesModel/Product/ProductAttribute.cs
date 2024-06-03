using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ProductAttribute(string name) : Entity()
{
    public string Name { get; private set; } = name.ToLower();
    private readonly List<ProductVariantAttributeValue> _productVariantAttributeValues = [];
    public ICollection<ProductVariantAttributeValue> ProductVariantAttributeValues => _productVariantAttributeValues;
    private readonly List<Product> _products = [];
    public ICollection<Product> Products => _products;
    private readonly List<ProductProductAttribute> _productProductAttributes = [];
    public ICollection<ProductProductAttribute> ProductProductAttributes => _productProductAttributes;
}
