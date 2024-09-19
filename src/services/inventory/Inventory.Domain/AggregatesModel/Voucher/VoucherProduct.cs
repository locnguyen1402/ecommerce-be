using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class VoucherProduct : Entity
{
    public Guid VoucherId { get; private set; }
    public Guid ProductId { get; private set; }
    public virtual Voucher Voucher { get; private set; } = null!;
    public virtual Product Product { get; private set; } = null!;
}