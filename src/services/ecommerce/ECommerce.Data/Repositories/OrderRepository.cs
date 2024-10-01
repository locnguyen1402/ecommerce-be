using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class OrderRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, Order>(dbContext), IOrderRepository
{
}