using Microsoft.AspNetCore.Identity;

namespace ECommerce.Inventory.Domain.AggregatesModel.Identity;

public class UserLogin : IdentityUserLogin<Guid>
{
    public virtual User User { get; set; } = null!;
}
