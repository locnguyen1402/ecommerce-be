using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Services;

public interface ICustomerService
{
    Task<Customer> GetCustomerInfoAsync(CancellationToken cancellationToken = default);

    Task<Customer> GetCustomerInfoByIdAsync(Guid id, CancellationToken cancellationToken = default);
}