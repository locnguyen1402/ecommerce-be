using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ProductVariant(int stock, decimal price) : Entity()
{
    public int Stock { get; private set; } = stock;
    public decimal Price { get; private set; } = price;
    public Guid ProductId { get; private set; }
    public Product? Product { get; set; }
    private readonly HashSet<ProductVariantAttributeValue> _productVariantAttributeValues = [];
    public IReadOnlyCollection<ProductVariantAttributeValue> ProductVariantAttributeValues => _productVariantAttributeValues;
    public readonly List<OrderItem> _orderItems = [];
    public virtual IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    public readonly List<ProductPromotionItem> _productPromotionItems = [];
    public virtual IReadOnlyCollection<ProductPromotionItem> ProductPromotionItems => _productPromotionItems;

    public void UpdatePrice(decimal price)
    {
        if (price < 0)
        {
            throw new Exception("Price cannot be negative");
        }
        Price = price;
    }
    public void UpdateStock(int stock)
    {
        if (stock < 0)
        {
            throw new Exception("Stock cannot be negative");
        }
        Stock = stock;
    }
    public void AddOrUpdateAttributeValue(Guid attributeId, string attributeValue, Guid? attributeValueId)
    {
        if (attributeValue.Trim().Length == 0)
        {
            throw new Exception("Attribute value cannot be empty");
        }

        var existingValue = _productVariantAttributeValues.FirstOrDefault(x => x.ProductAttributeId == attributeId);

        if (existingValue != null)
        {
            existingValue.UpdateValue(attributeValue, attributeValueId);
        }
        else
        {
            var value = new ProductVariantAttributeValue(attributeId, attributeValue, attributeValueId);

            _productVariantAttributeValues.Add(value);
        }
    }

    public void AddOrUpdateAttributeValue(ProductVariantAttributeValue value)
    {
        AddOrUpdateAttributeValue(value.ProductAttributeId, value.Value, value.AttributeValueId);
    }

    public void RemoveAttributeValue(Guid attributeId)
    {
        var value = _productVariantAttributeValues.FirstOrDefault(x => x.ProductAttributeId == attributeId);

        if (value != null)
        {
            _productVariantAttributeValues.Remove(value);
        }
    }
}