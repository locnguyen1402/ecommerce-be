using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Services;

public interface IMerchantService
{
    Task<Guid> GetMerchantIdAsync(
        CancellationToken cancellationToken = default);

    Task<Store> GetStoreInfoAsync(
        CancellationToken cancellationToken = default);
}