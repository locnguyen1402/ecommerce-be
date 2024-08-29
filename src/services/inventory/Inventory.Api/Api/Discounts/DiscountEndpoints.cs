using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Discounts.Queries;
using ECommerce.Inventory.Api.Discounts.Commands;

namespace ECommerce.Inventory.Api.Endpoints;

public class DiscountEndpoints(WebApplication app) : MinimalEndpoint(app, "/discounts")
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