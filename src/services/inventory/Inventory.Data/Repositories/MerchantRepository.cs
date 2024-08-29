using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class MerchantRepository(InventoryDbContext dbContext) : Repository<Merchant>(dbContext), IMerchantRepository
{
}