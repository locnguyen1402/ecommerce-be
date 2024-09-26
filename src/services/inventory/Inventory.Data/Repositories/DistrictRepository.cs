using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class DistrictRepository(InventoryDbContext dbContext) : Repository<InventoryDbContext, District>(dbContext), IDistrictRepository
{
}