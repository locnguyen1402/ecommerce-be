using ECommerce.Inventory.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Merchants.Application;

public static partial class GetDefaultMerchantQuery
{
    public static async ValueTask<Merchant> Execute(
        IMerchantRepository merchantRepository
        , CancellationToken cancellationToken = default)
    {
        var merchant = await merchantRepository.Query.FirstOrDefaultAsync(cancellationToken);

        if (merchant == null)
        {
            throw new Exception($"Can not found current merchant");
        }

        return merchant;
    }
}