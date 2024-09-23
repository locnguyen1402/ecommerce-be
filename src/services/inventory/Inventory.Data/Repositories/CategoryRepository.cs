using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class CategoryRepository(InventoryDbContext dbContext) : Repository<InventoryDbContext, Category>(dbContext), ICategoryRepository
{
}