namespace ECommerce.Inventory.Api.Services;

public interface IProductService
{
    Task<IDictionary<Guid, bool>> UpdateStockByProductVariantsAsync(
        IDictionary<Guid, int> productVariants,
        CancellationToken cancellationToken = default);
}