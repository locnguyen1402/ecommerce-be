using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class StoreCollectionProduct(Guid storeCollectionId, Guid productId) : Entity
{
    public Guid StoreCollectionId { get; private set; } = storeCollectionId;
    public Guid ProductId { get; private set; } = productId;
    public virtual StoreCollection StoreCollection { get; private set; } = null!;
    public virtual Product Product { get; private set; } = null!;
}