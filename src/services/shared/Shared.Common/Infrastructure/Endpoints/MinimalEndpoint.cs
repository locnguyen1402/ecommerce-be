using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Common.Endpoint;

public abstract class MinimalEndpoint : IMinimalEndpoint
{
    private readonly IMediator _mediator;
    protected IEndpointRouteBuilder Builder { get; private set; } = null!;
    public MinimalEndpoint(WebApplication app, string basePath)
    {
        _mediator = app.Services.GetRequiredService<IMediator>();
        Builder = app.MapGroup(basePath);
        MapEndpoints(_mediator);
    }
    public abstract void MapEndpoints(IMediator mediator);

}