using System.Security.Claims;

namespace ECommerce.Infrastructure.Services;

public interface IProfileService
{
    Task<IReadOnlyList<Claim>> GetProfileDataAsync(ClaimsPrincipal principal);
}
