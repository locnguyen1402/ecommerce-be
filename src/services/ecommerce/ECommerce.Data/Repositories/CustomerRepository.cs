using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories;

public class CustomerRepository(ECommerceDbContext dbContext) : Repository<ECommerceDbContext, Customer>(dbContext), ICustomerRepository
{
}