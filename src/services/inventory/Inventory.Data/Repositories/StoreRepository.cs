using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class StoreRepository(InventoryDbContext dbContext) : Repository<InventoryDbContext, Store>(dbContext), IStoreRepository
{
}