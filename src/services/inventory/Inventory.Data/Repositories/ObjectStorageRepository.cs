using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class ObjectStorageRepository(InventoryDbContext dbContext) : Repository<InventoryDbContext, ObjectStorage>(dbContext), IObjectStorageRepository
{
}