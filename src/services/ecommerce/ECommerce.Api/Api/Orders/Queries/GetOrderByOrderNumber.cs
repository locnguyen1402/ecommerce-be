using ECommerce.Api.Orders.Responses;
using ECommerce.Api.Orders.Specifications;
using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

namespace ECommerce.Api.Orders.Queries;

public class GetOrderByOrderNumberQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string orderNumber,
        IOrderRepository orderRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetOrderByOrderNumberSpecification<OrderResponse>(orderNumber, OrderProjection.ToOrderResponse());

        var order = await orderRepository.FindAsync(spec, cancellationToken);

        if (order is null)
        {
            return Results.NotFound();
        }

        return TypedResults.Ok(order);
    };
}