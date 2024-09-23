using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class VoucherRepository(InventoryDbContext dbContext) : Repository<InventoryDbContext, Voucher>(dbContext), IVoucherRepository
{
}