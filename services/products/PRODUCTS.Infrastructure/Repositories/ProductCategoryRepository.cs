namespace ECommerce.Products.Infrastructure.Repositories;
public class ProductCategoryRepository : EntityRepository<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}