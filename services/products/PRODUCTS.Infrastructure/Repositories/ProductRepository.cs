
namespace ECommerce.Products.Infrastructure.Repositories;
public class ProductRepository : EntityRepository<Product>, IProductRepository
{
    public ProductRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<Product> IncludedQuery => this.Query.Include(p => p.Category).Include(p => p.Tags);
}