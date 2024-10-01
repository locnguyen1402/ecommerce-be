using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using OpenIddict.Core;
using OpenIddict.Server.AspNetCore;

using ECommerce.Api.Identities.Connect.Requests;
using ECommerce.Domain.AggregatesModel.Identity;

using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Shared.Common.Infrastructure.Services;

namespace ECommerce.Api.Identities.Connect.Commands;

public class RevokeTokenCommandHandler : IEndpointHandler
{
    public Delegate Handle
    =>
    new Func<
        RevokeTokenRequest,
        IIdentityService,
        OpenIddictTokenManager<Token>,
        ILogger<RevokeTokenCommandHandler>,
        CancellationToken,
        Task<IResult>>(
            [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)] async (
                RevokeTokenRequest revokeRequest,
                IIdentityService identityService,
                OpenIddictTokenManager<Token> tokenManager,
                ILogger<RevokeTokenCommandHandler> logger,
                CancellationToken cancellationToken
            ) =>
        {
            var accessToken = identityService.AccessToken;

            // Find the access token.
            var accessTokenInstance = await tokenManager.FindByReferenceIdAsync(accessToken, cancellationToken);

            if (accessTokenInstance == null)
            {
                logger.LogError("Can not find token with access token {0}", accessToken);
                throw new InvalidOperationException("Token not found");
            }

            try
            {
                // Get the userId and application from the access token.
                var userId = accessTokenInstance.Subject;
                var authorization = accessTokenInstance.Authorization;

                if (!string.IsNullOrEmpty(userId) && authorization != null)
                {
                    var tokens = await tokenManager.FindBySubjectAsync(userId, cancellationToken).ToListAsync();

                    var applicationId = authorization.Application?.Id;

                    foreach (var token in tokens)
                    {
                        if (token.Application?.Id == applicationId)
                        {
                            await tokenManager.TryRevokeAsync(token, cancellationToken);
                        }
                    }
                }

                return TypedResults.NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to revoke token with error: {0}", ex.Message);
                throw new InvalidOperationException("Revoke token failed");
            }
        }
    );
}
