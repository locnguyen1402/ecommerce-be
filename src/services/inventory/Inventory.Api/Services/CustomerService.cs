using ECommerce.Inventory.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public async Task<Guid> GetCustomerIdAsync(CancellationToken cancellationToken = default)
    {
        // TODO: Add Identity to get current customerId
        var customer = await customerRepository.Query
            .OrderByDescending(x => x.CreatedAt)
            .ThenBy(x => x.FullName)
        .FirstOrDefaultAsync(cancellationToken);

        if (customer == null)
        {
            throw new Exception($"Can not found current customer");
        }

        return customer.Id;
    }

}