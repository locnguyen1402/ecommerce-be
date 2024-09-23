using ECommerce.Inventory.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories.Identity;

public class PermissionGroupRepository(IdentityDbContext dbContext) : Repository<IdentityDbContext, PermissionGroup>(dbContext), IPermissionGroupRepository
{
}
