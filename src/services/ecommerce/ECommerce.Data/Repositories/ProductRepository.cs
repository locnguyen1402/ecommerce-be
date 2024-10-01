using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Data;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class ProductRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, Product>(dbContext), IProductRepository
{
}