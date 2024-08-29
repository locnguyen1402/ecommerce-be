using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class DiscountRepository(InventoryDbContext dbContext) : Repository<Discount>(dbContext), IDiscountRepository
{
}