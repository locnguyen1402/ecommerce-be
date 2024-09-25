using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Products.Queries;
using ECommerce.Inventory.Api.Products.Commands;

namespace ECommerce.Inventory.Api.Endpoints;

public class ProductEndpoints(WebApplication app) : MinimalEndpoint(app, "/inventory/products")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("Products");
    }
    public override void MapEndpoints()
    {
        Builder.MapGet<GetProductsQueryHandler>("/");
        Builder.MapGet<FilterProductsQueryHandler>("/filter");
        Builder.MapPost<CreateProductCommandHandler>("/");

        Builder.MapGet<GetProductByIdQueryHandler>("/{id:Guid}");
        Builder.MapPut<UpdateProductCommandHandler>("/{id:Guid}");
        Builder.MapPut<ExtendStockCommandHandler>("/{id:Guid}/variants/{variantId:Guid}/extend-stock");

        Builder.MapGet<GetProductBySlugQueryHandler>("/{slug}");

        Builder.MapGet<GetProductAttributesQueryHandler>("/attributes");
        Builder.MapPost<CreateProductAttributeCommandHandler>("/attributes");
        Builder.MapPut<UpdateProductAttributeCommandHandler>("/attributes/{id:Guid}");
    }
}