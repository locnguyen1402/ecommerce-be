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
    private readonly List<ProductVariant> _productVariants = [];
    public ICollection<ProductVariant> ProductVariants => _productVariants;
    public readonly List<ProductAttribute> _productAttributes = [];
    public ICollection<ProductAttribute> ProductAttributes => _productAttributes;
    private readonly List<ProductProductAttribute> _productProductAttributes = [];
    public ICollection<ProductProductAttribute> ProductProductAttributes => _productProductAttributes;
}