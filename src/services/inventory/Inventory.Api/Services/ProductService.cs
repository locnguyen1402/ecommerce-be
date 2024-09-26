using ECommerce.Inventory.Api.Products.Responses;
using ECommerce.Inventory.Api.Products.Specifications;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Infrastructure.Templates.MassUpdates.Products;
using ECommerce.Shared.Common.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Services;

public class ProductService(
    IProductRepository productRepository
    , IProductVariantRepository productVariantRepository
    , IMerchantRepository merchantRepository
    , IIdentityService identityService
) : IProductService
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

    public async Task<List<ImportBaseInfoTemplate>> GetImportBaseInfoTemplateAsync(
        string? keyword
        , List<Guid>? shopCollectionIds
        , List<Guid>? notInShopCollectionIds
        , CancellationToken cancellationToken = default)
    {
        var merchantId = identityService.MerchantId;

        // TODO: remove these lines after integrate with identity service
        var merchant = await merchantRepository.Query
            .OrderByDescending(x => x.CreatedAt)
            .ThenBy(x => x.Name)
        .FirstOrDefaultAsync(cancellationToken);

        merchantId ??= merchant?.Id;

        var spec = new FilterProductsSpecification<ImportBaseInfoTemplate>(
            ProductProjection.ToImportBaseInfoTemplateResponse()
            , keyword
            , null
            , shopCollectionIds
            , notInShopCollectionIds
        );

        var products = await productRepository.GetAsync(spec, cancellationToken);

        if (products is null)
            return [];

        return products.ToList();
    }

    public async Task<List<ImportSalesInfoTemplate>> GetImportSalesInfoTemplateAsync(
        string? keyword
        , List<Guid>? shopCollectionIds
        , List<Guid>? notInShopCollectionIds
        , CancellationToken cancellationToken = default)
    {
        var merchantId = identityService.MerchantId;

        // TODO: remove these lines after integrate with identity service
        var merchant = await merchantRepository.Query
            .OrderByDescending(x => x.CreatedAt)
            .ThenBy(x => x.Name)
        .FirstOrDefaultAsync(cancellationToken);

        merchantId ??= merchant?.Id;

        var spec = new FilterProductVariantsSpecification<ImportSalesInfoTemplate>(
            ProductVariantProjection.ToImportSalesInfoTemplateResponse()
            , keyword
            , null
            , shopCollectionIds
            , notInShopCollectionIds
        );

        var products = await productVariantRepository.GetAsync(spec, cancellationToken);

        if (products is null)
            return [];

        return products.ToList();
    }
}
