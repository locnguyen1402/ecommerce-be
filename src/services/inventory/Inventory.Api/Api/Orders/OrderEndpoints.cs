using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Orders.Queries;
using ECommerce.Inventory.Api.Orders.Commands;

namespace ECommerce.Inventory.Api.Endpoints;

public class OrderEndpoints(WebApplication app) : MinimalEndpoint(app, "/inventory/orders")
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