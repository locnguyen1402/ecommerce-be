using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class ImportHistoryRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, ImportHistory>(dbContext), IImportHistoryRepository
{
}