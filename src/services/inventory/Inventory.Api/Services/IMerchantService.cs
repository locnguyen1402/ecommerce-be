namespace ECommerce.Inventory.Api.Services;

public interface IMerchantService
{
    Task<Guid> GetMerchantIdAsync(
        CancellationToken cancellationToken = default);
}