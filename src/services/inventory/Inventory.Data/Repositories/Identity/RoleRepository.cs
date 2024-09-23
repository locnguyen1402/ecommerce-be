using ECommerce.Inventory.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories.Identity;

public class RoleRepository(IdentityDbContext dbContext) : Repository<IdentityDbContext, Role>(dbContext), IRoleRepository
{
}
