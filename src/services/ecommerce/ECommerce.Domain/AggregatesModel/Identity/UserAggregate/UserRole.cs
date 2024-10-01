using Microsoft.AspNetCore.Identity;

namespace ECommerce.Domain.AggregatesModel.Identity;

public class UserRole : IdentityUserRole<Guid>
{
    public virtual User User { get; set; } = null!;
    public virtual Role Role { get; set; } = null!;
}
