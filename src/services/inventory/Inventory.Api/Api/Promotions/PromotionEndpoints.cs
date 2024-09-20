using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Promotions.Commands;

namespace ECommerce.Inventory.Api.Endpoints;

public class PromotionEndpoints(WebApplication app) : MinimalEndpoint(app, "/inventory/promotions")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("Promotions");
    }
    public override void MapEndpoints()
    {
        Builder.MapPost<CreateProductPromotionCommandHandler>("/discount");
        Builder.MapPost<CreateFlashSalePromotionHandler>("/flash-sale");
    }
}