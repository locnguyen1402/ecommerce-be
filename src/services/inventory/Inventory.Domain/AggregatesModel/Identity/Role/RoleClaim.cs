using Microsoft.AspNetCore.Identity;

namespace ECommerce.Inventory.Domain.AggregatesModel.Identity;

public class RoleClaim : IdentityRoleClaim<Guid>
{
    public virtual Role Role { get; set; } = null!;
}
