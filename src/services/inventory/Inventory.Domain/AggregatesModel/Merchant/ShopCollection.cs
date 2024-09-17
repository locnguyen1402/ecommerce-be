using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ShopCollection(string name, string slug, Guid? parentId) : Entity
{
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = slug;
    public Guid? ParentId { get; private set; } = parentId;
    public ShopCollection? Parent { get; private set; }
    public Guid MerchantId { get; private set; }
    public virtual Merchant Merchant { get; private set; } = null!;
    public readonly List<Product> _products = [];
    public virtual IReadOnlyCollection<Product> Products => _products;

    public void Update(string name, string slug, Guid? parentId)
    {
        Name = name;
        Slug = slug;
        ParentId = parentId;
    }

    public void SetMerchant(Guid merchantId)
    {
        MerchantId = merchantId;
    }

    public void AddProduct(Product product)
    {
        var existedProduct = _products.FirstOrDefault(x => x.Id == product.Id);
        if (existedProduct == null)
        {
            _products.Add(product);
        }
    }

    public void RemoveProduct(Product product)
    {
        var existedProduct = _products.FirstOrDefault(x => x.Id == product.Id);
        if (product != null)
        {
            _products.Remove(product);
        }
    }
}