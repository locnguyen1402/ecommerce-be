using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ProductAttribute(string name) : Entity()
{
    public string Name { get; private set; } = name;
    private readonly List<ProductVariantAttributeValue> _productVariantAttributeValues = [];
    public ICollection<ProductVariantAttributeValue> ProductVariantAttributeValues => _productVariantAttributeValues;
}
