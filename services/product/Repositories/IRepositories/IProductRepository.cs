using ECommerce.Shared.Common;

namespace ECommerce.Services.Product;

public interface IProductRepository : IEntityRepository<Product>
{
    ValueTask<Product?> GetProductDetail(Guid id);
}