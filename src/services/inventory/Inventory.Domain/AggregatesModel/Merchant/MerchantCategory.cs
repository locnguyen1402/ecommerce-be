using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class MerchantCategory(Guid merchantId, Guid categoryId) : Entity
{
    public Guid MerchantId { get; private set; } = merchantId;
    public Guid CategoryId { get; private set; } = categoryId;
    public virtual Merchant Merchant { get; private set; } = null!;
    public virtual Category Category { get; private set; } = null!;
}