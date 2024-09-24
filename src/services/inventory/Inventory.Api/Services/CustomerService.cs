using Microsoft.EntityFrameworkCore;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Services;

namespace ECommerce.Inventory.Api.Services;

public class CustomerService(ICustomerRepository customerRepository, IIdentityService identityService) : ICustomerService
{
    public async Task<Customer> GetCustomerInfoAsync(CancellationToken cancellationToken = default)
    {
        var customerId = identityService.CustomerId;
        var customer = await customerRepository.Query
                .Include(x => x.Contacts)
                .OrderByDescending(x => x.CreatedAt)
                .ThenBy(x => x.FullName)
            .Where(x => x.Id == customerId)
            .FirstOrDefaultAsync(cancellationToken);

        if (customer == null)
        {
            throw new Exception($"Can not found current customer");
        }

        return customer;
    }

    public async Task<Customer> GetCustomerInfoByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var customer = await customerRepository.Query
                .Include(x => x.Contacts)
                .OrderByDescending(x => x.CreatedAt)
                .ThenBy(x => x.FullName)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (customer == null)
        {
            throw new Exception($"Can not found current customer");
        }

        return customer;
    }
}