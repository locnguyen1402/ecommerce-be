using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class MerchantCategory : Entity
{
    public Guid MerchantId { get; private set; }
    public Guid CategoryId { get; private set; }
    public virtual Merchant Merchant { get; private set; } = null!;
    public virtual Category Category { get; private set; } = null!;

    private MerchantCategory() { }

    public MerchantCategory(Guid merchantId, Guid categoryId) : this()
    {
        MerchantId = merchantId;
        CategoryId = categoryId;
    }
}