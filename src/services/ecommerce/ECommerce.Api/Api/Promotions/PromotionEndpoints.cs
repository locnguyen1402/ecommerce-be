using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Api.Promotions.Commands;
using ECommerce.Shared.Common.Constants;

namespace ECommerce.Api.Endpoints;

public class PromotionEndpoints(WebApplication app) : MinimalEndpoint(app, $"/{EndpointConstants.PATH_SEGMENT}/promotions")
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