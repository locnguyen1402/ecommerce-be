using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class ProductAttributeRepository(InventoryDbContext dbContext) : Repository<InventoryDbContext, ProductAttribute>(dbContext), IProductAttributeRepository
{
}