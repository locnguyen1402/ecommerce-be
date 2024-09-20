using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class OrderPromotionRepository(InventoryDbContext dbContext) : Repository<OrderPromotion>(dbContext), IOrderPromotionRepository
{
}