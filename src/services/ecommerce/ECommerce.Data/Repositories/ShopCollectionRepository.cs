using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class ShopCollectionRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, ShopCollection>(dbContext), IShopCollectionRepository
{
}