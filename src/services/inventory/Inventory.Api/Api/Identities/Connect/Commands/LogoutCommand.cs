using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

using ECommerce.Inventory.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Inventory.Api.Identities.Connect.Requests;
using Microsoft.AspNetCore.Authentication;

namespace ECommerce.Inventory.Api.Identities.Connect.Commands;

public class LogoutCommandHandler : IEndpointHandler
{
    public Delegate Handle
    =>
    new Func<
        LogoutRequest,
        SignInManager<User>,
        IHttpContextAccessor,
        ILogger<LogoutCommandHandler>,
        CancellationToken,
        Task<IResult>>(
            [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)] async (
                LogoutRequest logoutRequest,
                SignInManager<User> signInManager,
                IHttpContextAccessor httpContextAccessor,
                ILogger<LogoutCommandHandler> logger,
                CancellationToken cancellationToken
            ) =>
        {
            logger.LogInformation("Logout command received");

            var httpContext = httpContextAccessor.HttpContext
                ?? throw new InvalidOperationException("The HTTP context cannot be retrieved.");

            OpenIddictRequest request = httpContext.GetOpenIddictServerRequest()
                ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            await signInManager.SignOutAsync();

            return TypedResults.SignOut(
                properties: new AuthenticationProperties
                {
                    RedirectUri = request.PostLogoutRedirectUri ?? "/"
                },
                authenticationSchemes: [OpenIddictServerAspNetCoreDefaults.AuthenticationScheme]
            );
        }
    );
}
