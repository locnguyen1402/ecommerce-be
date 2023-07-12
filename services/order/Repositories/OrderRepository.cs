using ECommerce.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Orders;

public class OrderRepository : EntityRepository<Order>, IOrderRepository
{
    private readonly OrderDbContext _dbContext;
    public OrderRepository(OrderDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async ValueTask<Order?> GetOrderDetail(Guid id)
    {
        return await Query.FirstOrDefaultAsync(p => p.Id == id);
    }
}