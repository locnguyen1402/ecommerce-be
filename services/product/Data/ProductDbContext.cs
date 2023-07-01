using ECommerce.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Product;

public class ProductDbContext : BaseDbContext
{
    public ProductDbContext(DbContextOptions opts) : base(opts)
    {
    }
}