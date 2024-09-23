using ECommerce.Inventory.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories.Identity;

public class PermissionRepository(IdentityDbContext dbContext) : Repository<IdentityDbContext, Permission>(dbContext), IPermissionRepository
{
}
