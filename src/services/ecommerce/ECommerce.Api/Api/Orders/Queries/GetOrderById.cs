using ECommerce.Api.Orders.Responses;
using ECommerce.Api.Orders.Specifications;
using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

namespace ECommerce.Api.Orders.Queries;

public class GetOrderByIdQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        IOrderRepository orderRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetOrderByIdSpecification<OrderResponse>(id, OrderProjection.ToOrderResponse());

        var order = await orderRepository.FindAsync(spec, cancellationToken);

        if (order is null)
        {
            return Results.NotFound();
        }

        return TypedResults.Ok(order);
    };
}