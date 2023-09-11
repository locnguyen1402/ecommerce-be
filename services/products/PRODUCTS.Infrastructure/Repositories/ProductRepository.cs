namespace ECommerce.Products.Infrastructure.Repositories;
public class ProductRepository : EntityRepository<Product>, IProductRepository
{
    private readonly ProductDbContext _dbContext;
    public ProductRepository(ProductDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async ValueTask<Product?> GetProductDetail(Guid id)
    {
        return await Query.FirstOrDefaultAsync(p => p.Id == id);
    }
}