using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class ContactRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, Contact>(dbContext), IContactRepository
{
}