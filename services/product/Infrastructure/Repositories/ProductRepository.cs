using ECommerce.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Product;

public class ProductRepository : EntityRepository<Product>, IProductRepository
{
    private readonly ProductDbContext _dbContext;
    public ProductRepository(ProductDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async ValueTask<Product?> GetProductDetail(Guid id)
    {
        return await Query.Include(p => p.ProductSaleInfo).FirstOrDefaultAsync(p => p.Id == id);
    }
}