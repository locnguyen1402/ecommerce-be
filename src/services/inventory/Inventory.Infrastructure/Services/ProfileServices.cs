using System.Security.Claims;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ECommerce.Inventory.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Constants;

namespace ECommerce.Inventory.Infrastructure.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ILogger<ProfileService> _logger;

    public ProfileService(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        ILogger<ProfileService> logger
    )
    {
        ArgumentNullException.ThrowIfNull(userManager);
        ArgumentNullException.ThrowIfNull(roleManager);
        ArgumentNullException.ThrowIfNull(logger);

        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task<IReadOnlyList<Claim>> GetProfileDataAsync(ClaimsPrincipal principal)
    {
        User? user = await FindUserAsync(principal);

        if (user == null)
        {
            _logger.LogError("User not found.");

            throw new NullReferenceException(nameof(user));
        }

        IEnumerable<Claim> personalUserClaims = GetPersonalClaims(user);
        IEnumerable<Claim> userClaims = await GetUserClaims(user);
        IEnumerable<Claim> roleClaims = await GetRoleClaims(user);
        IEnumerable<Claim> roleIdClaims = await GetRoleIdClaims(user);
        IEnumerable<Claim> systemAdminClaims = await GetSystemAdminClaims(user);
        IEnumerable<Claim> permissionClaims = await GetPermissionClaims(user);

        return personalUserClaims
            .Union(userClaims)
            .Union(permissionClaims)
            .Union(roleClaims)
            .Union(roleIdClaims)
            .Union(systemAdminClaims)
            .ToList();
    }

    private async Task<User?> FindUserAsync(ClaimsPrincipal principal)
    {
        Claim? subject = principal.Claims.FirstOrDefault(x => x.Type == "sub");

        if (subject == null)
            throw new NullReferenceException("Invalid subject identifier");

        return await _userManager.FindByIdAsync(subject.Value);
    }

    private IEnumerable<Claim> GetPersonalClaims(User user)
    {
        List<Claim> claims = new()
        {
            new Claim(SecurityClaimTypes.Subject, user.Id.ToString())
        };

        if (!string.IsNullOrWhiteSpace(user.UserName))
        {
            claims.Add(new Claim(SecurityClaimTypes.Name, user.UserName));
            claims.Add(new Claim(SecurityClaimTypes.PreferredUserName, user.UserName));
        }

        if (!string.IsNullOrWhiteSpace(user.FirstName))
            claims.Add(new Claim(SecurityClaimTypes.GivenName, user.FirstName));

        if (!string.IsNullOrWhiteSpace(user.LastName))
            claims.Add(new Claim(SecurityClaimTypes.FamilyName, user.LastName));

        if (!string.IsNullOrWhiteSpace(user.FullName))
            claims.Add(new Claim(SecurityClaimTypes.FullName, user.FullName));

        if (user.BirthDate != null)
            claims.Add(new Claim(SecurityClaimTypes.BirthDate, user.BirthDate!.ToString()!));

        claims.Add(new Claim(SecurityClaimTypes.Gender, user.Gender.ToString()));

        if (user.Picture != null)
            claims.Add(new Claim(SecurityClaimTypes.Picture, user.Picture));

        if (_userManager.SupportsUserEmail && !string.IsNullOrWhiteSpace(user.Email))
        {
            claims.AddRange(new[]
            {
                new Claim(SecurityClaimTypes.Email, user.Email),
                new Claim(SecurityClaimTypes.EmailVerified, user.EmailConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
            });
        }

        if (_userManager.SupportsUserPhoneNumber && !string.IsNullOrWhiteSpace(user.PhoneNumber))
        {
            claims.AddRange(new[]
            {
                    new Claim(SecurityClaimTypes.PhoneNumber, user.PhoneNumber),
                    new Claim(SecurityClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
                });
        }

        return claims;
    }

    private async Task<IEnumerable<Claim>> GetRoleClaims(User user)
    {
        List<Claim> claims = new();

        if (_userManager.SupportsUserRole)
        {
            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(
                roles.Select(role => new Claim(SecurityClaimTypes.Role, role))
            );
        }

        return claims;
    }

    private async Task<IEnumerable<Claim>> GetRoleIdClaims(User user)
    {
        List<Claim> claims = new();

        if (_userManager.SupportsUserRole)
        {
            var roleIds = await _userManager.Users
                                          .Include(x => x.UserRoles)
                                          .Where(x => x.Id == user.Id)
                                          .SelectMany(x => x.UserRoles.Select(y => y.RoleId))
                                          .ToListAsync();

            claims.AddRange(
                roleIds.Select(role => new Claim("roleIds", role.ToString()))
            );
        }

        return claims;
    }

    private async Task<IEnumerable<Claim>> GetSystemAdminClaims(User user)
    {
        List<Claim> claims = new();

        if (_userManager.SupportsUserRole)
        {
            var isSystemAdmin = await _userManager.IsInRoleAsync(user, "Administrator");

            claims.Add(new Claim("isSystemAdmin", isSystemAdmin.ToString()));
        }

        return claims;
    }

    private async Task<IEnumerable<Claim>> GetPermissionClaims(User user)
    {
        List<string> permissions = new();

        if (_userManager.SupportsUserRole && _roleManager.SupportsRoleClaims)
        {
            var rolePermissions = await _roleManager.Roles.AsNoTracking()
                .Include(x => x.UserRoles.Where(ur => ur.UserId == user.Id))
                .Include(x => x.RoleClaims.Where(x => x.ClaimType == SecurityClaimTypes.Permission && !string.IsNullOrEmpty(x.ClaimValue)))
                .SelectMany(x => x.RoleClaims)
                .Select(x => x.ClaimValue!)
                .ToListAsync();

            if (rolePermissions != null)
                permissions.AddRange(rolePermissions);
        }

        if (_userManager.SupportsUserClaim)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userPermissions = userClaims
                .Where(t => t.Type == SecurityClaimTypes.Permission)
                .Select(x => x.Value);

            permissions.AddRange(userPermissions);
        }

        return permissions
            .Distinct()
            .Select(permission => new Claim(SecurityClaimTypes.Permission, permission));
    }

    public async Task<IEnumerable<Claim>> GetUserClaims(User user)
    {
        if (!_userManager.SupportsUserClaim)
            return new List<Claim>();

        return await _userManager.GetClaimsAsync(user);
    }
}
