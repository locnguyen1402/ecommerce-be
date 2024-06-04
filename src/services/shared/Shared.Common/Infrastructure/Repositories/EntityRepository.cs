using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Shared.Common.Infrastructure.Repositories;
public class EntityRepository<TEntity>(BaseDbContext dbContext) : BaseRepository<TEntity>(dbContext), IEntityRepository<TEntity> where TEntity : Entity
{
    public async ValueTask<bool> IsExisted(Guid id)
    {
        return await _dbSet.AnyAsync(x => x.Id == id);
    }
}