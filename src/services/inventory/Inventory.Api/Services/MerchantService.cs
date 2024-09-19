using ECommerce.Inventory.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Services;

public class MerchantService(IMerchantRepository merchantRepository) : IMerchantService
{
    private readonly IMerchantRepository _merchantRepository = merchantRepository ?? throw new ArgumentNullException(nameof(merchantRepository));

    public async Task<Guid> GetMerchantIdAsync(CancellationToken cancellationToken = default)
    {
        // TODO: Add Identity to get current merchantId
        var merchant = await _merchantRepository.Query
            .OrderByDescending(x => x.CreatedAt)
            .ThenBy(x => x.Name)
        .FirstOrDefaultAsync(cancellationToken);

        if (merchant == null)
        {
            throw new Exception($"Can not found current merchant");
        }

        return merchant.Id;
    }
}
