using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Merchant(string name, string slug) : Entity
{
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = slug;
    public string? MerchantNumber { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public readonly List<Store> _stores = [];
    public virtual IReadOnlyCollection<Store> Stores => _stores;
    public readonly List<MerchantCategory> _categories = [];
    public virtual IReadOnlyCollection<MerchantCategory> Categories => _categories;
    public readonly List<MerchantProduct> _products = [];
    public virtual IReadOnlyCollection<MerchantProduct> Products => _products;
    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public void AddOrUpdateProducts(List<MerchantProduct> products)
    {
        _products.Clear();
        _products.AddRange(products.Count > 0 ? products : []);
    }

    public void AddOrUpdateCategories(List<MerchantCategory> categories)
    {
        _categories.Clear();
        _categories.AddRange(categories.Count > 0 ? categories : []);
    }
}