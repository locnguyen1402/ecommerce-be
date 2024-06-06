using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Product(string name, string slug, string? description) : Entity()
{
    public string Slug { get; private set; } = slug;
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description ?? string.Empty;
    private readonly List<Category> _categories = [];
    public ICollection<Category> Categories => _categories;
    private readonly List<CategoryProduct> _categoryProducts = [];
    public ICollection<CategoryProduct> CategoryProducts => _categoryProducts;
    private readonly HashSet<ProductVariant> _productVariants = [];
    public ICollection<ProductVariant> ProductVariants => _productVariants;
    private readonly List<ProductAttribute> _productAttributes = [];
    public ICollection<ProductAttribute> ProductAttributes => _productAttributes;
    private readonly List<ProductProductAttribute> _productProductAttributes = [];
    public ICollection<ProductProductAttribute> ProductProductAttributes => _productProductAttributes;
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
    public void AddVariant(ProductVariant variant)
    {
        _productVariants.Add(variant);
    }
    public void RemoveVariant(ProductVariant variant)
    {
        var test = _productVariants.Remove(variant);
        Console.WriteLine(test);
    }
}