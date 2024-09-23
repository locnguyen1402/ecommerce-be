using Microsoft.AspNetCore.Identity;

namespace ECommerce.Inventory.Domain.AggregatesModel.Identity;

public class UserClaim : IdentityUserClaim<Guid>
{
    public virtual User User { get; private set; } = null!;
}
