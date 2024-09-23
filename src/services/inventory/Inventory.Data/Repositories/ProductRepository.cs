using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Data;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class ProductRepository(InventoryDbContext dbContext) : Repository<InventoryDbContext, Product>(dbContext), IProductRepository
{
}