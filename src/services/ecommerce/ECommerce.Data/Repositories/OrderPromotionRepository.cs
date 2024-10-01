using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class OrderPromotionRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, OrderPromotion>(dbContext), IOrderPromotionRepository
{
}