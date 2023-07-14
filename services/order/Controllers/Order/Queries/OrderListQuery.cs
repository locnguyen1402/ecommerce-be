using ECommerce.Shared.Common;

namespace ECommerce.Services.Orders;

public class OrderListQuery : PaginationQuery
{
    public string? keyword { get; set; }
}