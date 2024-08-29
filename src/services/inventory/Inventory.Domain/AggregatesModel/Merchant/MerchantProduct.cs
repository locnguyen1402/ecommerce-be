using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class MerchantProduct(Guid merchantId, Guid productId) : Entity
{
    public Guid MerchantId { get; private set; } = merchantId;
    public Guid ProductId { get; private set; } = productId;
    public virtual Merchant Merchant { get; private set; } = null!;
    public virtual Product Product { get; private set; } = null!;
}