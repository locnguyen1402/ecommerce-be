using MediatR;

using ECommerce.Shared.Common.Endpoint;

using ECommerce.Inventory.Api.Products.Queries;

namespace ECommerce.Inventory.Api.Endpoints;

public class ProductEndpoints(WebApplication app) : MinimalEndpoint(app, "/products")
{
    public override void MapEndpoints(IMediator mediator)
    {
        Builder.MapGet("/", () => mediator.Send(new GetProductsQuery()));

        Builder.MapGet("/{id}", (string id) => mediator.Send(new GetProductByIdQuery(id)));
    }
}