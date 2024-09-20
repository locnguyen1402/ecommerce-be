using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class VoucherProduct(Guid voucherId, Guid productId) : Entity
{
    public Guid VoucherId { get; private set; } = voucherId;
    public virtual Voucher Voucher { get; private set; } = null!;
    public Guid ProductId { get; private set; } = productId;
    public virtual Product Product { get; private set; } = null!;
}
