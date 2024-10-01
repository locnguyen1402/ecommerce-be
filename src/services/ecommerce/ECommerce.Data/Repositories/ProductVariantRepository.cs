using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Data;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class ProductVariantRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, ProductVariant>(dbContext), IProductVariantRepository
{
}