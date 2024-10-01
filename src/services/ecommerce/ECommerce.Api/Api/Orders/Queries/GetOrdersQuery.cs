using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Orders.Specifications;
using ECommerce.Api.Orders.Responses;

namespace ECommerce.Api.Orders.Queries;

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
