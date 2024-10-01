using System.Linq.Expressions;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Orders.Responses;

public record OrderResponse(
    Guid Id,
    string OrderNumber
)
{
};

public static class OrderProjection
{
    public static OrderResponse ToOrderResponse(this Order order)
    {
        return ToOrderResponse().Compile().Invoke(order);
    }

    public static Expression<Func<Order, OrderResponse>> ToOrderResponse()
        => x =>
        new OrderResponse(
            x.Id,
            x.OrderNumber
        );
}