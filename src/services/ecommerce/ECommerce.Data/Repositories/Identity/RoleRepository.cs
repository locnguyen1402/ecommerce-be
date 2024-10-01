using ECommerce.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories.Identity;

public class RoleRepository(IdentityDbContext dbContext) : Repository<IdentityDbContext, Role>(dbContext), IRoleRepository
{
}
