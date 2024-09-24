using ECommerce.Shared.Common.AggregatesModel.Auditing;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ShopCollection(string name, string slug, string? description, Guid? parentId) : AuditedAggregateRoot
{
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = slug;
    public string Description { get; private set; } = description ?? string.Empty;
    public Guid? ParentId { get; private set; } = parentId;
    public ShopCollection? Parent { get; private set; }
    public Guid MerchantId { get; private set; }
    public virtual Merchant Merchant { get; private set; } = null!;
    private readonly List<ShopCollection> _shopCollections = [];
    public IReadOnlyCollection<ShopCollection> ShopCollections => _shopCollections;
    public readonly List<ShopCollectionProduct> _shopCollectionProducts = [];
    public virtual IReadOnlyCollection<ShopCollectionProduct> ShopCollectionProducts => _shopCollectionProducts;
    public readonly List<Product> _products = [];
    public virtual IReadOnlyCollection<Product> Products => _products;
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
    public void SetMerchant(Guid merchantId)
    {
        MerchantId = merchantId;
    }
    public void ReplaceProducts(List<Product> products)
    {
        _products.Clear();
        _products.AddRange(products);
    }
    public void AddOrUpdateChildren(List<ShopCollection> shopCollections)
    {
        _shopCollections.RemoveAll(x => !shopCollections.Any(a => a.Id == x.Id));

        foreach (var shopCollection in shopCollections)
        {
            if (!_shopCollections.Any(x => x.Id == shopCollection.Id))
            {
                _shopCollections.Add(shopCollection);
            }
        }
    }
    public void AddProducts(List<Product> products)
    {
        _products.AddRange(products);
    }
    public void RemoveProducts(List<Guid> productIds)
    {
        _shopCollectionProducts.RemoveAll(x => productIds.Contains(x.ProductId));
    }
}