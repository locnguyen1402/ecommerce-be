using ECommerce.Shared.Common;

namespace ECommerce.Services.Product;

public class ProductRepository : EntityRepository<Product>, IProductRepository
{
    private readonly ProductDbContext _dbContext;
    public ProductRepository(ProductDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}