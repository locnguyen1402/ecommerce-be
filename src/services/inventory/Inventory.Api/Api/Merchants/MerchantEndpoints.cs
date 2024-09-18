using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Merchants.Queries;
using ECommerce.Inventory.Api.Merchants.Commands;

namespace ECommerce.Inventory.Api.Endpoints;

public class MerchantEndpoints(WebApplication app) : MinimalEndpoint(app, "/inventory/merchants")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("Merchants");
    }
    public override void MapEndpoints()
    {
        Builder.MapGet<GetMerchantsQueryHandler>("/");
        Builder.MapPost<CreateMerchantCommandHandler>("/");

        Builder.MapGet<GetMerchantByIdQueryHandler>("/{id:Guid}");
        Builder.MapPut<UpdateMerchantCommandHandler>("/{id:Guid}");

    }
}