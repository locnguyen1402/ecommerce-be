using ECommerce.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories.Identity;

public class ClientRoleRepository(IdentityDbContext dbContext) : Repository<IdentityDbContext, ClientRole>(dbContext), IClientRoleRepository
{
}
