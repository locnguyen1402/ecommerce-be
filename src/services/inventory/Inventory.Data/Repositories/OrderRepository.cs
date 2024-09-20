using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class OrderRepository(InventoryDbContext dbContext) : Repository<Order>(dbContext), IOrderRepository
{
}