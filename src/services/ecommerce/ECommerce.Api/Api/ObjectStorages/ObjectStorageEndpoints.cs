using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Api.ObjectStorages.Queries;
using ECommerce.Shared.Common.Constants;

namespace ECommerce.Api.Endpoints;

public class ObjectStorageEndpoints(WebApplication app) : MinimalEndpoint(app, $"/{EndpointConstants.PATH_SEGMENT}/object-storages")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("ObjectStorages");
    }
    public override void MapEndpoints()
    {
        Builder.MapGet<GetFileQueryHandler>("/{bucket}/{key}.{ext}");
    }
}