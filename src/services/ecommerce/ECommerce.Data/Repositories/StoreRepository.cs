using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class StoreRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, Store>(dbContext), IStoreRepository
{
}