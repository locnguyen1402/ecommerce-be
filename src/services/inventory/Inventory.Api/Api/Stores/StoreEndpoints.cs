using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.Stores.Queries;
using ECommerce.Inventory.Api.Stores.Commands;

namespace ECommerce.Inventory.Api.Endpoints;

public class StoreEndpoints(WebApplication app) : MinimalEndpoint(app, "/inventory/stores")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("Stores");
    }
    public override void MapEndpoints()
    {
        Builder.MapGet<GetStoresQueryHandler>("/");
        Builder.MapPost<CreateStoreCommandHandler>("/");

        Builder.MapGet<GetStoreByIdQueryHandler>("/{id:Guid}");
        Builder.MapPut<UpdateStoreCommandHandler>("/{id:Guid}");

    }
}