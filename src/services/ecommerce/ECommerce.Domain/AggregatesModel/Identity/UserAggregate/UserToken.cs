using Microsoft.AspNetCore.Identity;

namespace ECommerce.Domain.AggregatesModel.Identity;

public class UserToken : IdentityUserToken<Guid>
{
    public virtual User User { get; set; } = null!;
}
