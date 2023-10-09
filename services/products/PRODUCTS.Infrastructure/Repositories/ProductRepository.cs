namespace ECommerce.Products.Infrastructure.Repositories;
public class ProductRepository : EntityRepository<Product>, IProductRepository
{
    public ProductRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}