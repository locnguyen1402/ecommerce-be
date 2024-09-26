using ECommerce.Inventory.Infrastructure.Templates.MassUpdates.Products;

namespace ECommerce.Inventory.Api.Services;

public interface IProductService
{
    Task<IDictionary<Guid, bool>> UpdateStockByProductVariantsAsync(
        IDictionary<Guid, int> productVariants,
        CancellationToken cancellationToken = default);

    Task<List<ImportBaseInfoTemplate>> GetImportBaseInfoTemplateAsync(
        string? keyword
        , List<Guid>? shopCollectionIds
        , List<Guid>? notInShopCollectionIds
        , CancellationToken cancellationToken = default);

    Task<List<ImportSalesInfoTemplate>> GetImportSalesInfoTemplateAsync(
        string? keyword
        , List<Guid>? shopCollectionIds
        , List<Guid>? notInShopCollectionIds
        , CancellationToken cancellationToken = default);
}