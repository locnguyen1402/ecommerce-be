using ECommerce.Inventory.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Services;

public class MerchantService(IMerchantRepository merchantRepository) : IMerchantService
{
    public async Task<Guid> GetMerchantIdAsync(CancellationToken cancellationToken = default)
    {
        // TODO: Add Identity to get current merchantId
        var merchant = await merchantRepository.Query
            .OrderByDescending(x => x.CreatedAt)
            .ThenBy(x => x.Name)
        .FirstOrDefaultAsync(cancellationToken);

        if (merchant == null)
        {
            throw new Exception($"Can not found current merchant");
        }

        return merchant.Id;
    }

    public async Task<Store> GetStoreInfoAsync(CancellationToken cancellationToken = default)
    {
        var store = await merchantRepository.Query
           .Include(x => x.Stores)
           .OrderByDescending(x => x.CreatedAt)
           .ThenBy(x => x.Name)
       .SelectMany(x => x.Stores)
       .FirstOrDefaultAsync(cancellationToken);

        if (store == null)
        {
            throw new Exception($"Can not found store");
        }

        return store;
    }
}
