using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class VoucherRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, Voucher>(dbContext), IVoucherRepository
{
}