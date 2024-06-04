using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Common.Infrastructure.Endpoint;

public abstract class MinimalEndpoint : IMinimalEndpoint
{
    protected IEndpointRouteBuilder Builder { get; private set; } = null!;
    public MinimalEndpoint(WebApplication app, string basePath)
    {
        Builder = app.MapGroup(basePath);
        MapEndpoints();
    }
    public abstract void MapEndpoints();
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