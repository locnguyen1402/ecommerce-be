using ECommerce.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Infrastructure.Repositories;

namespace ECommerce.Data.Repositories.Identity;

public class UserRepository(IdentityDbContext dbContext) : Repository<IdentityDbContext, User>(dbContext), IUserRepository
{
}