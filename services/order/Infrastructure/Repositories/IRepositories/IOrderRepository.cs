using ECommerce.Shared.Common;

namespace ECommerce.Services.Orders;

public interface IOrderRepository : IEntityRepository<Order>
{
    ValueTask<Order?> GetOrderDetail(Guid id);
}