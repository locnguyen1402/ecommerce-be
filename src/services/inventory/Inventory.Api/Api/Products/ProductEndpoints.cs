using MediatR;

using ECommerce.Shared.Common.Endpoint;

using ECommerce.Shared.Common.Queries;

using ECommerce.Inventory.Api.Products.Queries;
using ECommerce.Inventory.Api.Products.Commands;

namespace ECommerce.Inventory.Api.Endpoints;

public class ProductEndpoints(WebApplication app) : MinimalEndpoint(app, "/products")
{
    public override void MapEndpoints(IMediator mediator)
    {
        Builder.MapPost("/attributes", (CreateProductAttributeCommand command) => mediator.Send(command));

        Builder.MapGet("/", (PagingQuery query) => mediator.Send(new GetProductsQuery(query)));

        Builder.MapGet("/{id:Guid}", (Guid id) => mediator.Send(new GetProductByIdQuery(id)));
    }
}