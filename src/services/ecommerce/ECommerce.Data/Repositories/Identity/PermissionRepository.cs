using ECommerce.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories.Identity;

public class PermissionRepository(IdentityDbContext dbContext) : Repository<IdentityDbContext, Permission>(dbContext), IPermissionRepository
{
}
