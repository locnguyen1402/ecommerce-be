using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Shared.Common.Infrastructure.Repositories;
public class EntityRepository<TEntity> : BaseRepository<TEntity>, IEntityRepository<TEntity> where TEntity : Entity
{
    public EntityRepository(BaseDbContext dbContext) : base(dbContext)
    {
    }
    public async ValueTask<bool> IsExisted(Guid id)
    {
        return await _dbSet.AnyAsync(x => x.Id == id);
    }
}