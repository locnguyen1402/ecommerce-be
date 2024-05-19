using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace ECommerce.Shared.Common.Endpoint;

public abstract class MinimalEndpoint : IMinimalEndpoint
{
    protected IEndpointRouteBuilder Builder { get; private set; } = null!;
    public MinimalEndpoint(IEndpointRouteBuilder builder, string basePath)
    {
        Builder = builder;
        Builder.MapGroup(basePath);
        MapEndpoints();
    }
    public abstract void MapEndpoints();
}