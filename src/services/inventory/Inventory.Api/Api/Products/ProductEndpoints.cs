using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Products.Queries;
using ECommerce.Inventory.Api.Products.Commands;

namespace ECommerce.Inventory.Api.Endpoints;

public class ProductEndpoints(WebApplication app) : MinimalEndpoint(app, "/products")
{
    public override void MapEndpoints()
    {
        Builder.MapGet<GetProductsQueryHandler>("/");
        Builder.MapPost<CreateProductCommandHandler>("/");
        // Builder.MapGet("/{id:Guid}", (Guid id) => mediator.Send(new GetProductByIdQuery(id)));
        Builder.MapPost<CreateProductAttributeCommandHandler>("/attributes");


    }
}