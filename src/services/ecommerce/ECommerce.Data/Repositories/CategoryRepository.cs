using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class CategoryRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, Category>(dbContext), ICategoryRepository
{
}