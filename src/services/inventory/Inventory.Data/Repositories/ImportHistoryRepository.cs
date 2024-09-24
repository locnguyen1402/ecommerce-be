using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class ImportHistoryRepository(InventoryDbContext dbContext) : Repository<InventoryDbContext, ImportHistory>(dbContext), IImportHistoryRepository
{
}