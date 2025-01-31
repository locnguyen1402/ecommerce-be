using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Data;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class CategoryRepository(InventoryDbContext dbContext) : EntityRepository<Category>(dbContext), ICategoryRepository
{
}