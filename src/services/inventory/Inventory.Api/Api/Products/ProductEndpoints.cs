using MediatR;

using ECommerce.Shared.Common.Endpoint;

using ECommerce.Inventory.Api.Products.Queries;
using ECommerce.Shared.Common.Queries;

namespace ECommerce.Inventory.Api.Endpoints;

public class ProductEndpoints(WebApplication app) : MinimalEndpoint(app, "/products")
{
    public override void MapEndpoints(IMediator mediator)
    {
        Builder.MapGet("/", (PagingQuery query) => mediator.Send(new GetProductsQuery(query)));

        Builder.MapGet("/{id:Guid}", (Guid id) => mediator.Send(new GetProductByIdQuery(id)));
    }
}