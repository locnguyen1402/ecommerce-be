using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class ProductPromotionRepository(InventoryDbContext dbContext) : Repository<InventoryDbContext, ProductPromotion>(dbContext), IProductPromotionRepository
{
}