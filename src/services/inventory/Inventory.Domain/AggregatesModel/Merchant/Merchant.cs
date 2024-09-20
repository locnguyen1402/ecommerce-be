using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Merchant(string name, string slug) : Entity
{
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = slug;
    public string? MerchantNumber { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; } = true;
    public readonly List<Store> _stores = [];
    public virtual IReadOnlyCollection<Store> Stores => _stores;
    public readonly List<Category> _categories = [];
    public virtual IReadOnlyCollection<Category> Categories => _categories;
    public readonly List<MerchantCategory> _merchantCategories = [];
    public virtual IReadOnlyCollection<MerchantCategory> MerchantCategories => _merchantCategories;
    public readonly List<Product> _products = [];
    public virtual IReadOnlyCollection<Product> Products => _products;
    public readonly List<ShopCollection> _shopCollections = [];
    public virtual IReadOnlyCollection<ShopCollection> ShopCollections => _shopCollections;
    public readonly List<OrderPromotion> _orderPromotions = [];
    public virtual IReadOnlyCollection<OrderPromotion> OrderPromotions => _orderPromotions;
    public readonly List<ProductPromotion> _productPromotions = [];
    public virtual IReadOnlyCollection<ProductPromotion> ProductPromotions => _productPromotions;

    public void Update(string name, string slug, string? description)
    {
        Name = name;
        Slug = slug;
        Description = description;
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

    public void AddStore(Store store)
    {
        _stores.Add(store);
    }
}