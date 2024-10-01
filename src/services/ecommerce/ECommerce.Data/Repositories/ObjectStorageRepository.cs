using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class ObjectStorageRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, ObjectStorage>(dbContext), IObjectStorageRepository
{
}