using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class ProductAttributeRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, ProductAttribute>(dbContext), IProductAttributeRepository
{
}