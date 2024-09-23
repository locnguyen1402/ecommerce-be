using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Inventory.Api.Identities.Connect.Commands;
namespace ECommerce.Inventory.Api.Endpoints;

/// <summary>
/// Represents the Connect endpoint.
/// </summary>
public class ConnectEndpoint(WebApplication app) : MinimalEndpoint(app, "")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("Identities");
    }

    public override void MapEndpoints()
    {
        Builder.MapPost<ExchangeTokenCommandHandler>("/connect/token").DisableAntiforgery();
    }
}
