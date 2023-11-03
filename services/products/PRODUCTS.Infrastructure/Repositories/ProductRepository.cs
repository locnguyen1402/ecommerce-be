
namespace ECommerce.Products.Infrastructure.Repositories;
public class ProductRepository : EntityRepository<Product>, IProductRepository
{
    public ProductRepository(ProductDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<Product> IncludedQuery => Query
                                                    .Include(p => p.Category)
                                                    .Include(p => p.Tags)
                                                    .Include(p => p.ProductTags)
                                                        .ThenInclude(p => p.Tag);
}