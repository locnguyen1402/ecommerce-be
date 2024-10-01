using System.Security.Claims;
using ECommerce.Api.Identities.Connect.Requests;
using ECommerce.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Infrastructure.Endpoint;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace ECommerce.Api.Identities.Connect.Commands;

public class ExchangeTokenCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => new Func<
        ExchangeTokenRequest,
        SignInManager<User>,
        UserManager<User>,
        OpenIddictScopeManager<Scope>,
        IClientRoleRepository,
        IHttpContextAccessor,
        ILogger<ExchangeTokenCommandHandler>,
        CancellationToken,
        Task<IResult>>(
            async (
            [FromForm] ExchangeTokenRequest tokenRequest,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            OpenIddictScopeManager<Scope> scopeManager,
            IClientRoleRepository clientRoleRepository,
            //IProfileService profileService,
            IHttpContextAccessor httpContextAccessor,
            //IMediator mediator,
            ILogger<ExchangeTokenCommandHandler> logger,
            CancellationToken cancellationToken
        ) =>
        {
            logger.LogDebug("Exchange token command received.");

            var httpContext = httpContextAccessor.HttpContext
                ?? throw new InvalidOperationException("The HTTP context cannot be retrieved.");

            OpenIddictRequest request = httpContext.GetOpenIddictServerRequest()
                ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            var requestScopes = request.GetScopes();

            if (request.IsPasswordGrantType())
            {
                logger.LogDebug("Password grant type received.");

                var user = await userManager.FindByNameAsync(request.Username!);

                if (user is null)
                {
                    return TypedResults.Forbid(
                        properties: new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The username/password couple is invalid."
                        })
                        , authenticationSchemes: [OpenIddictServerAspNetCoreDefaults.AuthenticationScheme]
                    );
                }

                // Ensure the user can connect to application.
                var roles = await userManager.GetRolesAsync(user);
                var stringRoles = string.Join(",", roles);
                bool checkClientRole = await clientRoleRepository
                        .Query.AnyAsync(x => x.ClientId == request.ClientId
                                        && stringRoles.Contains(x.RoleName), cancellationToken);

                if (!checkClientRole)
                {
                    return TypedResults.Forbid(
                        properties: new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "User have not role to connect this application."
                        })
                        , authenticationSchemes: [OpenIddictServerAspNetCoreDefaults.AuthenticationScheme]
                    );
                }

                // Validate the username/password parameters and ensure the account is not locked out.
                var result = await signInManager.CheckPasswordSignInAsync(user, request.Password!, lockoutOnFailure: true);

                if (!result.Succeeded)
                {
                    if (result.RequiresTwoFactor)
                    {
                        logger.LogWarning("Two-factor authentication is required.");

                        return TypedResults.Forbid(
                            properties: new AuthenticationProperties(new Dictionary<string, string?>
                            {
                                [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "Two-factor authentication is required."
                            })
                            , authenticationSchemes: [OpenIddictServerAspNetCoreDefaults.AuthenticationScheme]
                        );
                    }

                    if (result.IsLockedOut)
                    {
                        logger.LogWarning("The user account is locked out.");

                        return TypedResults.Forbid(
                            properties: new AuthenticationProperties(new Dictionary<string, string?>
                            {
                                [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The user account is locked out."
                            })
                            , authenticationSchemes: [OpenIddictServerAspNetCoreDefaults.AuthenticationScheme]
                        );
                    }

                    return TypedResults.Forbid(
                        properties: new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "Invalid credentials were specified."
                        })
                        , authenticationSchemes: [OpenIddictServerAspNetCoreDefaults.AuthenticationScheme]
                    );
                }

                // Create the claims-based identity that will be used by OpenIddict to generate tokens.
                var identity = new ClaimsIdentity(
                    authenticationType: TokenValidationParameters.DefaultAuthenticationType,
                    nameType: Claims.Name,
                    roleType: Claims.Role
                );

                identity.SetClaim(Claims.Subject, user.Id.ToString());

                // Add the claims that will be persisted in the tokens.
                // var userInfo = await mediator.SendAsync(new GetUserInfoQuery
                // {
                //     Id = user.Id
                // }, cancellationToken);

                // if (userInfo is not null)
                // {
                //     foreach (var item in userInfo)
                //     {
                //         identity.SetClaim(item.Key, item.Value.ToString());
                //     }
                // }

                // Note: in this sample, the granted scopes match the requested scope
                // but you may want to allow the user to uncheck specific scopes.
                // For that, simply restrict the list of scopes before calling SetScopes.
                identity.SetScopes(request.GetScopes());
                identity.SetResources(await scopeManager.ListResourcesAsync(identity.GetScopes(), cancellationToken).ToListAsync());

                // Add the user's store, if available.
                user.UserClaims.ToList().ForEach(x => identity.SetClaim(x.ClaimType ?? string.Empty, x.ClaimValue));

                var principal = new ClaimsPrincipal(identity);

                // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
                return TypedResults.SignIn(principal, authenticationScheme: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }
            else if (request.IsAuthorizationCodeGrantType() || request.IsDeviceCodeGrantType() || request.IsRefreshTokenGrantType())
            {
                logger.LogDebug("Authorization code/device code/refresh token grant type received.");

                // Retrieve the claims principal stored in the authorization code/device code/refresh token.
                var result = await httpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                // Retrieve the user profile corresponding to the authorization code/refresh token.
                var user = await userManager.FindByIdAsync(result.Principal?.GetClaim(Claims.Subject) ?? string.Empty);

                if (user is null)
                {
                    return TypedResults.Forbid(
                        properties: new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The token is no longer valid."
                        })
                        , authenticationSchemes: [OpenIddictServerAspNetCoreDefaults.AuthenticationScheme]
                    );
                }

                // Ensure the user can connect to application.
                var roles = await userManager.GetRolesAsync(user);
                var stringRoles = string.Join(",", roles);
                bool checkClientRole = await clientRoleRepository
                        .Query.AnyAsync(x => x.ClientId == request.ClientId
                                        && stringRoles.Contains(x.RoleName), cancellationToken);

                if (!checkClientRole)
                {
                    return TypedResults.Forbid(
                        properties: new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "User have not role to connect this application."
                        })
                        , authenticationSchemes: [OpenIddictServerAspNetCoreDefaults.AuthenticationScheme]
                    );
                }

                // Ensure the user is still allowed to sign in.
                if (!await signInManager.CanSignInAsync(user))
                {
                    return TypedResults.Forbid(
                        properties: new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The user is no longer allowed to sign in."
                        })
                        , authenticationSchemes: [OpenIddictServerAspNetCoreDefaults.AuthenticationScheme]
                    );
                }

                var identity = new ClaimsIdentity(result.Principal?.Claims,
                    authenticationType: TokenValidationParameters.DefaultAuthenticationType,
                    nameType: Claims.Name,
                    roleType: Claims.Role
                );

                identity.SetClaim(Claims.Subject, user.Id.ToString());

                // Override the user claims present in the principal in case they
                // changed since the authorization code/refresh token was issued.
                // var userInfo = await mediator.SendAsync(new GetUserInfoQuery
                // {
                //     Id = user.Id
                // }, cancellationToken);
                // if (userInfo is not null)
                // {
                //     foreach (var item in userInfo)
                //     {
                //         identity.SetClaim(item.Key, item.Value.ToString());
                //     }
                // }

                identity.SetScopes(request.GetScopes());
                identity.SetResources(await scopeManager.ListResourcesAsync(identity.GetScopes(), cancellationToken).ToListAsync());

                // Add the user's store, if available.
                user.UserClaims.ToList().ForEach(x => identity.SetClaim(x.ClaimType ?? string.Empty, x.ClaimValue));

                var principal = new ClaimsPrincipal(identity);

                // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
                return TypedResults.SignIn(principal, authenticationScheme: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            logger.LogDebug("Unsupported grant type received.");

            //throw new InvalidOperationException("The specified grant type is not supported.");
            return TypedResults.BadRequest("The specified grant type is not supported.");
        }
    );
}
