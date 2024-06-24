using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ECommerce.Shared.Common.Infrastructure.Endpoint;

public abstract class MinimalEndpoint : IMinimalEndpoint
{
    protected string BasePath { get; private set; }
    protected IEndpointRouteBuilder Builder { get; private set; } = null!;
    public MinimalEndpoint(WebApplication app, string basePath)
    {
        BasePath = basePath;
        var groupBuilder = app.MapGroup(basePath);
        Builder = groupBuilder;
        Configure(groupBuilder);
        MapEndpoints();
    }
    public abstract void MapEndpoints();

    public virtual void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags(BasePath);
    }
}

public static class MinimalEndpointExtensions
{
    public static RouteHandlerBuilder MapGet<THandler>(this IEndpointRouteBuilder builder, string basePath = "") where THandler : IEndpointHandler, new()
        => builder.MapGet(basePath, new THandler().Handle);
    public static RouteHandlerBuilder MapPost<THandler>(this IEndpointRouteBuilder builder, string basePath = "") where THandler : IEndpointHandler, new()
        => builder.MapPost(basePath, new THandler().Handle);
    public static RouteHandlerBuilder MapPut<THandler>(this IEndpointRouteBuilder builder, string basePath = "") where THandler : IEndpointHandler, new()
        => builder.MapPut(basePath, new THandler().Handle);
    public static RouteHandlerBuilder MapPatch<THandler>(this IEndpointRouteBuilder builder, string basePath = "") where THandler : IEndpointHandler, new()
        => builder.MapPatch(basePath, new THandler().Handle);
    public static RouteHandlerBuilder MapDelete<THandler>(this IEndpointRouteBuilder builder, string basePath = "") where THandler : IEndpointHandler, new()
        => builder.MapDelete(basePath, new THandler().Handle);
}