using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class ProductPromotionRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, ProductPromotion>(dbContext), IProductPromotionRepository
{
}