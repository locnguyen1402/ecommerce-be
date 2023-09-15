namespace ECommerce.Products.Infrastructure.IRepositories;
public interface IProductRepository : IEntityRepository<Product>
{
    ValueTask<Product?> GetProductDetail(Guid id);
}