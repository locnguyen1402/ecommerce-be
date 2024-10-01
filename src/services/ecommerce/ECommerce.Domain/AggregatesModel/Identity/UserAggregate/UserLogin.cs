using Microsoft.AspNetCore.Identity;

namespace ECommerce.Domain.AggregatesModel.Identity;

public class UserLogin : IdentityUserLogin<Guid>
{
    public virtual User User { get; set; } = null!;
}
