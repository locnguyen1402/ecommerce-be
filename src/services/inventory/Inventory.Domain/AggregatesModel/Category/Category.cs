using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Category(string name, string slug, string description, Guid? parentId) : Entity()
{
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = slug;
    public string Description { get; private set; } = description;
    public Guid? ParentId { get; private set; } = parentId;
    public Category? Parent { get; private set; }
    private readonly List<Category> _categories = [];
    public ICollection<Category> Categories => _categories;
    private readonly List<CategoryProduct> _categoryProducts = [];
    public ICollection<CategoryProduct> CategoryProducts => _categoryProducts;
    private readonly List<Product> _products = [];
    public ICollection<Product> Products => _products;
    public void ChangeParent(Guid parentId)
    {
        ParentId = parentId;
    }
    public void ChangeDescription(string description)
    {
        Description = description;
    }
    public void AddChild(Category category)
    {
        _categories.Add(category);
    }
    public void AddChildren(IList<Category> categories)
    {
        foreach (var category in categories)
        {
            _categories.Add(category);
        }
    }
}