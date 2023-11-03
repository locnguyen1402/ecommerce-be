namespace ECommerce.Products.Infrastructure.Repositories;
public interface IProductRepository : IEntityRepository<Product>
{
    IQueryable<Product> IncludedQuery { get; }
}