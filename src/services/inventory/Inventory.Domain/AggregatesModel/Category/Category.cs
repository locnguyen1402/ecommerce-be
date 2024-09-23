namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Category(string name, string slug, string? description, Guid? parentId) : EntityWithDiscounts
{
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = slug;
    public string Description { get; private set; } = description ?? string.Empty;
    public Guid? ParentId { get; private set; } = parentId;
    public Category? Parent { get; private set; }
    private readonly List<Category> _categories = [];
    public IReadOnlyCollection<Category> Categories => _categories;
    public readonly List<Merchant> _merchants = [];
    public virtual IReadOnlyCollection<Merchant> Merchants => _merchants;
    public readonly List<MerchantCategory> _merchantCategories = [];
    public virtual IReadOnlyCollection<MerchantCategory> MerchantCategories => _merchantCategories;
    public void ChangeParent(Guid? parentId)
    {
        ParentId = parentId;
    }
    public void UpdateGeneralInfo(string name, string slug, string? description)
    {
        Name = name;
        Slug = slug;
        Description = description ?? string.Empty;
    }
    public void AddChild(Category category)
    {
        _categories.Add(category);
    }
    public void AddChildren(IList<Category> categories)
    {
        foreach (var category in categories)
        {
            AddChild(category);
        }
    }
    public void AddOrUpdateChildren(List<Category> categories)
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
}