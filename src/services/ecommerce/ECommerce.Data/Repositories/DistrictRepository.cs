using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class DistrictRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, District>(dbContext), IDistrictRepository
{
}