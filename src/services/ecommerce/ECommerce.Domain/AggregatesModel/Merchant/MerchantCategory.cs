using ECommerce.Shared.Common.AggregatesModel.Auditing;

namespace ECommerce.Domain.AggregatesModel;

public class MerchantCategory : AuditedAggregateRoot
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