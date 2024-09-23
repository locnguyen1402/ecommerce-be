using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

using ECommerce.Inventory.Domain.AggregatesModel.Identity;
using ECommerce.Inventory.Infrastructure.Services;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

namespace ECommerce.Inventory.Api.Identities.Connect.Queries;

public class GetUserInfoQuery : IEndpointHandler
{
    public Delegate Handle
    =>
    [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
    async (
        UserManager<User> userManager,
        IProfileService profileService,
        IHttpContextAccessor httpContextAccessor,
        ILogger<GetUserInfoQuery> logger,
        CancellationToken cancellationToken
    ) =>
    {
        logger.LogInformation("Getting user info.");

        var httpContext = httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("The HTTP context cannot be retrieved.");

        var userId = httpContext.User.GetClaim(Claims.Subject) ?? string.Empty;

        if (string.IsNullOrEmpty(userId))
        {
            logger.LogError("Invalid user id.");

            return null;
        }

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            logger.LogError("User not found.");

            return null;
        }

        var claims = await profileService.GetProfileDataAsync(httpContext.User);

        var data = claims.GroupBy(c => c.Type).ToDictionary(k => k.Key, v =>
        {
            if (v.Count() > 1)
                return v.Select(x => x.Value).ToArray();
            else
                return v.ElementAt(0).Value as object;
        });

        if (data is null)
        {
            return Results.Challenge(
                properties: new AuthenticationProperties(new Dictionary<string, string?>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidToken,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                        "The specified access token is bound to an account that no longer exists."
                })
                , authenticationSchemes: [OpenIddictServerAspNetCoreDefaults.AuthenticationScheme]
            );
        }

        return TypedResults.Ok(data);
    };
}