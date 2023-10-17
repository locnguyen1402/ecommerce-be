namespace ECommerce.Products.Infrastructure.Repositories;
public class CategoryRepository : EntityRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }
}