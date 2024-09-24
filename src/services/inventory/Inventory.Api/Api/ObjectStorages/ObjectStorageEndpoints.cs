using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Api.ObjectStorages.Queries;

namespace ECommerce.Inventory.Api.Endpoints;

public class ObjectStorageEndpoints(WebApplication app) : MinimalEndpoint(app, "/inventory/object-storages")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("ObjectStorages");
    }
    public override void MapEndpoints()
    {
        Builder.MapGet<GetFileQueryHandler>("/{bucket}/{prefix}/{year}/{month}/{day}/{key}.{ext}");
    }
}