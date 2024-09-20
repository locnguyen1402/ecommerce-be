using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Orders.Specifications;
using ECommerce.Inventory.Api.Orders.Responses;

namespace ECommerce.Inventory.Api.Orders.Queries;

public class GetOrdersQueryHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        string? keyword,
        PagingQuery pagingQuery,
        IOrderRepository orderRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var spec = new GetOrdersSpecification<OrderResponse>(
            OrderProjection.ToOrderResponse()
            , keyword
            , pagingQuery
        );

        var items = await orderRepository.PaginateAsync(spec, cancellationToken);

        return Results.Extensions.PaginatedListOk(items);
    };
}
