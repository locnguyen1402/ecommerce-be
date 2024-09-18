using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ShopCollectionProduct : Entity
{
    public Guid ShopCollectionId { get; private set; }
    public Guid ProductId { get; private set; }
    public virtual ShopCollection ShopCollection { get; private set; } = null!;
    public virtual Product Product { get; private set; } = null!;

    private ShopCollectionProduct()
    {
    }

    public ShopCollectionProduct(Guid collectionId, Guid productId) : this()
    {
        ShopCollectionId = collectionId;
        ProductId = productId;
    }
}