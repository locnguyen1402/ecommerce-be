using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Api.Orders.Queries;
using ECommerce.Api.Orders.Commands;
using ECommerce.Shared.Common.Constants;

namespace ECommerce.Api.Endpoints;

public class OrderEndpoints(WebApplication app) : MinimalEndpoint(app, $"/{EndpointConstants.PATH_SEGMENT}/orders")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("Orders");
    }
    public override void MapEndpoints()
    {
        Builder.MapGet<GetOrdersQueryHandler>("/");
        Builder.MapPost<CreateOrderCommandHandler>("/");
    }
}