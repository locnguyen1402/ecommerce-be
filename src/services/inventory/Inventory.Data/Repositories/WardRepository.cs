using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class WardRepository(InventoryDbContext dbContext) : Repository<InventoryDbContext, Ward>(dbContext), IWardRepository
{
}