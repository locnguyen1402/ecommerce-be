using ECommerce.Inventory.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Services;

public class ProductService(IProductVariantRepository productVariantRepository) : IProductService
{
    public async Task<IDictionary<Guid, bool>> UpdateStockByProductVariantsAsync(IDictionary<Guid, int> orderItems, CancellationToken cancellationToken = default)
    {
        var productVariantIds = orderItems.Keys.ToList();

        var productVariants = await productVariantRepository.Query
            .Where(x => productVariantIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var result = new Dictionary<Guid, bool>();

        productVariants.ForEach(x =>
        {
            var quantity = orderItems[x.Id];

            result[x.Id] = x.Stock >= quantity;
            if (result[x.Id])
            {
                x.DecreaseStock(quantity);
                productVariantRepository.Update(x);
            }
        });

        await productVariantRepository.SaveChangesAsync(cancellationToken);

        return result;

    }
}
