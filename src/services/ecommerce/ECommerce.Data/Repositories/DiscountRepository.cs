using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class DiscountRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, Discount>(dbContext), IDiscountRepository
{
}