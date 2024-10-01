using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class ProvinceRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, Province>(dbContext), IProvinceRepository
{
}