using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class ShopCollectionRepository(InventoryDbContext dbContext) : Repository<InventoryDbContext, ShopCollection>(dbContext), IShopCollectionRepository
{
}