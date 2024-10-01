using ECommerce.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories.Identity;

public class PermissionGroupRepository(IdentityDbContext dbContext) : Repository<IdentityDbContext, PermissionGroup>(dbContext), IPermissionGroupRepository
{
}
