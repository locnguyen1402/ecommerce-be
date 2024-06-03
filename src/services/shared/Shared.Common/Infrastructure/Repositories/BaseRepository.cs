using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Shared.Common.Infrastructure.Repositories;
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly BaseDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;
    public IQueryable<TEntity> Query => _dbSet;
    public BaseRepository(BaseDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }
    public async ValueTask<TEntity?> FindAsync(object keyValues)
    {
        return await _dbSet.FindAsync(keyValues);
    }
    public async ValueTask<TEntity?> FindAsync(object[] keyValues)
    {
        return await _dbSet.FindAsync(keyValues);
    }
    public TEntity Add(TEntity entity)
    {
        var entityEntry = _dbSet.Add(entity);

        return entityEntry.Entity;
    }
    public TEntity Update(TEntity entity)
    {
        var entityEntry = _dbSet.Update(entity);

        return entityEntry.Entity;
    }
    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
    // public async ValueTask<bool> IsExisted(Guid id)
    // {
    //     return await _dbSet.AnyAsync(x => x.Id == id);
    // }
}