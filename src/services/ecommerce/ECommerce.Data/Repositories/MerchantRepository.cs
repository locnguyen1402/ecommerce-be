using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class MerchantRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, Merchant>(dbContext), IMerchantRepository
{
}