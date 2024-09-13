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
    public readonly List<MerchantProduct> _products = [];
    public virtual IReadOnlyCollection<MerchantProduct> Products => _products;

    public void Update(string name)
    {
        Name = name;
    }

    public void SetMerchant(Guid merchantId)
    {
        MerchantId = merchantId;
    }

    public void AddProduct(MerchantProduct product)
    {
        _products.Add(product);
    }
}