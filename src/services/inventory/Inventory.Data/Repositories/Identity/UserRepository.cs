using ECommerce.Inventory.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Inventory.Data.Repositories.Identity;

public class UserRepository(IdentityDbContext dbContext) : Repository<IdentityDbContext, User>(dbContext), IUserRepository
{
}