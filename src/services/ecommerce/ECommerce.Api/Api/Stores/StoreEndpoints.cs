using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Api.Stores.Queries;
using ECommerce.Api.Stores.Commands;
using ECommerce.Shared.Common.Constants;

namespace ECommerce.Api.Endpoints;

public class StoreEndpoints(WebApplication app) : MinimalEndpoint(app, $"/{EndpointConstants.PATH_SEGMENT}/stores")
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