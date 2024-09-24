using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Services;

public interface ICustomerService
{
    Task<Customer> GetCustomerInfoAsync(CancellationToken cancellationToken = default);

    Task<Customer> GetCustomerInfoByIdAsync(Guid id, CancellationToken cancellationToken = default);
}