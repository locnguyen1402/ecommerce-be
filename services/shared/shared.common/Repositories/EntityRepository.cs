using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common;

namespace ECommerce.Shared.Common;

public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : Entity
{
    private readonly BaseDbContext _dbContext;

    private readonly DbSet<TEntity> _dbSet;

    public IQueryable<TEntity> Query => _dbSet.AsNoTracking();

    public EntityRepository(BaseDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async ValueTask<TEntity> AddAsync(TEntity entity)
    {
        var entityEntry = await _dbSet.AddAsync(entity);

        await SaveChangesAsync();

        return entityEntry.Entity;
    }
    public async ValueTask<TEntity> UpdateAsync(TEntity entity)
    {
        var entityEntry = _dbSet.Update(entity);

        await SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async ValueTask<TEntity?> FindAsync(object keyValues)
    {
        return await _dbSet.FindAsync(keyValues);
    }
    public async ValueTask<TEntity?> FindAsync(object[] keyValues)
    {
        return await _dbSet.FindAsync(keyValues);
    }

    public async Task RemoveAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await SaveChangesAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}