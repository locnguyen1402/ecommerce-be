using ECommerce.Shared.Common.AggregatesModel.Auditing;

namespace ECommerce.Domain.AggregatesModel;

public class VoucherProduct(Guid voucherId, Guid productId) : AuditedAggregateRoot
{
    public Guid VoucherId { get; private set; } = voucherId;
    public virtual Voucher Voucher { get; private set; } = null!;
    public Guid ProductId { get; private set; } = productId;
    public virtual Product Product { get; private set; } = null!;
}
