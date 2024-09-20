namespace ECommerce.Inventory.Api.Services;

public interface ICustomerService
{
    Task<Guid> GetCustomerIdAsync(
        CancellationToken cancellationToken = default);
}