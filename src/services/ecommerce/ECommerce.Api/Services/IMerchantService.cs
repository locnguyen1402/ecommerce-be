using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Services;

public interface IMerchantService
{
    Task<Guid> GetMerchantIdAsync(
        CancellationToken cancellationToken = default);

    Task<Store> GetStoreInfoAsync(
        CancellationToken cancellationToken = default);
}