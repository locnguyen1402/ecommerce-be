namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Product(string name, string slug, string? description) : EntityWithDiscounts
{
    public string Slug { get; private set; } = slug;
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description ?? string.Empty;
    // public decimal Price => ProductVariants.Min(x => x.Price);
    private readonly List<Category> _categories = [];
    public IReadOnlyCollection<Category> Categories => _categories;
    private readonly List<CategoryProduct> _categoryProducts = [];
    public IReadOnlyCollection<CategoryProduct> CategoryProducts => _categoryProducts;
    private readonly HashSet<ProductVariant> _productVariants = [];
    public IReadOnlyCollection<ProductVariant> ProductVariants => _productVariants;
    private readonly List<ProductAttribute> _productAttributes = [];
    public IReadOnlyCollection<ProductAttribute> ProductAttributes => _productAttributes;
    private readonly List<ProductProductAttribute> _productProductAttributes = [];
    public IReadOnlyCollection<ProductProductAttribute> ProductProductAttributes => _productProductAttributes;
    public Guid MerchantId { get; private set; }
    public virtual Merchant Merchant { get; private set; } = null!;

    public void UpdateGeneralInfo(string name, string slug, string? description)
    {
        Name = name;
        Slug = slug;
        Description = description ?? string.Empty;
    }
    public void AddAttribute(ProductAttribute attribute)
    {
        if (_productAttributes.Any(x => x.Id == attribute.Id))
        {
            return;
        }

        _productAttributes.Add(attribute);
    }
    public void AddOrUpdateAttributes(IEnumerable<ProductAttribute> attributes)
    {
        _productAttributes.RemoveAll(x => !attributes.Any(a => a.Id == x.Id));

        foreach (var attribute in attributes)
        {
            if (!_productAttributes.Any(x => x.Id == attribute.Id))
            {
                _productAttributes.Add(attribute);
            }
        }
    }
    public void AddOrUpdateCategories(IEnumerable<Category> categories)
    {
        _categories.RemoveAll(x => !categories.Any(a => a.Id == x.Id));

        foreach (var category in categories)
        {
            if (!_categories.Any(x => x.Id == category.Id))
            {
                _categories.Add(category);
            }
        }
    }
    public void AddVariant(int stock, decimal price, List<ProductVariantAttributeValue> attributeValues)
    {
        var variant = new ProductVariant(stock, price);

        foreach (var value in attributeValues)
        {
            variant.AddOrUpdateAttributeValue(value);
        }

        _productVariants.Add(variant);
    }
    public void RemoveVariant(ProductVariant variant)
    {
        _productVariants.Remove(variant);
    }
    public void RemoveVariants(List<ProductVariant> variants)
    {
        foreach (var variant in variants)
        {
            _productVariants.Remove(variant);
        }
    }

    public void SetMerchant(Guid merchantId)
    {
        MerchantId = merchantId;
    }
}