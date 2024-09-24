﻿using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Inventory.Api.Identities.Connect.Commands;
using ECommerce.Inventory.Api.Identities.Connect.Queries;
namespace ECommerce.Inventory.Api.Endpoints;

/// <summary>
/// Represents the connect endpoint.
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
        Builder.MapPost<RevokeTokenCommandHandler>("/connect/revocation");
        Builder.MapPost<LogoutCommandHandler>("/connect/endsession");

        Builder.MapGet<GetUserInfoQuery>("/connect/userinfo");
    }
}
