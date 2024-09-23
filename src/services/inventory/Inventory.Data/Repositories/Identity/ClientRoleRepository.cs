using ECommerce.Inventory.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories.Identity;

public class ClientRoleRepository(IdentityDbContext dbContext) : Repository<IdentityDbContext, ClientRole>(dbContext), IClientRoleRepository
{
}
