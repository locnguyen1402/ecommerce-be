using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class WardRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, Ward>(dbContext), IWardRepository
{
}