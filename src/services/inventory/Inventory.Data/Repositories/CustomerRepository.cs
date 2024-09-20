using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories;

public class CustomerRepository(InventoryDbContext dbContext) : Repository<Customer>(dbContext), ICustomerRepository
{
}