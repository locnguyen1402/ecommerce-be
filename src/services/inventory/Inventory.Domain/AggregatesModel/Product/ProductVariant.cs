using ECommerce.Shared.Common.Helper;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ProductVariant(int stock, decimal price) : Entity()
{
    public int Stock { get; private set; } = stock;
    public decimal Price { get; private set; } = price;
    public Guid ProductId { get; private set; }
    public Product? Product { get; set; }
    private readonly HashSet<ProductVariantAttributeValue> _productVariantAttributeValues = [];
    public ICollection<ProductVariantAttributeValue> ProductVariantAttributeValues => _productVariantAttributeValues;
    public void UpdatePrice(decimal price)
    {
        if (price < 0)
        {
            throw new Exception("Price cannot be negative");
        }
        Price = price;
    }
    public void UpdateInStock(int stock)
    {
        if (stock < 0)
        {
            throw new Exception("Stock cannot be negative");
        }
        Stock = stock;
    }
    public void AddAttributeValue(ProductVariantAttributeValue value)
    {
        _productVariantAttributeValues.Add(value);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (ProductVariant)obj;
        return ProductVariantAttributeValues
            .ToHashSet()
            .SetEquals(other.ProductVariantAttributeValues.ToHashSet());
    }

    public override int GetHashCode()
        => HashCode.Combine(HashCodeHelper.GetListHashCode(ProductVariantAttributeValues, true));
}