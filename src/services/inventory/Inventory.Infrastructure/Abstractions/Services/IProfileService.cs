using System.Security.Claims;

namespace ECommerce.Inventory.Infrastructure.Services;

public interface IProfileService
{
    Task<IReadOnlyList<Claim>> GetProfileDataAsync(ClaimsPrincipal principal);
}
