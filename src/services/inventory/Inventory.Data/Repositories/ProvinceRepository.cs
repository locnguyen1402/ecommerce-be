using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class ProvinceRepository(InventoryDbContext dbContext) : Repository<InventoryDbContext, Province>(dbContext), IProvinceRepository
{
}