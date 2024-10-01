using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Api.Discounts.Queries;
using ECommerce.Api.Discounts.Commands;

using ECommerce.Shared.Common.Constants;

namespace ECommerce.Api.Endpoints;

public class DiscountEndpoints(WebApplication app) : MinimalEndpoint(app, $"/{EndpointConstants.PATH_SEGMENT}/discounts")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("Discounts");
    }
    public override void MapEndpoints()
    {
        Builder.MapGet<GetDiscountsQueryHandler>("/");

        Builder.MapPost<CreateDiscountCommandHandler>("/");
        Builder.MapPut<UpdateDiscountCommandHandler>("/{id:Guid}");

    }
}