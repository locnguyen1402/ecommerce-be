using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

using ECommerce.Shared.Common.Infrastructure.Data;
using ECommerce.Shared.Common.Infrastructure.Specification;
using ECommerce.Shared.Common.Extensions;

namespace ECommerce.Shared.Common.Infrastructure.Repositories;
public class BaseRepository<TEntity>(BaseDbContext dbContext) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly BaseDbContext _dbContext = dbContext;
    protected readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();
    public IQueryable<TEntity> Query => _dbSet;
    public async ValueTask<TEntity?> FindAsync(object keyValues, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync([keyValues], cancellationToken);
    public async ValueTask<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync(keyValues, cancellationToken);
    public async ValueTask<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await _dbSet.AnyAsync(predicate, cancellationToken);
    public async ValueTask<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await _dbSet.CountAsync(predicate, cancellationToken);
    public TEntity Add(TEntity entity)
        => _dbSet.Add(entity).Entity;
    public async ValueTask<TEntity> AddAndSaveChangeAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var addedEntity = Add(entity);

        await SaveChangesAsync(cancellationToken);

        return addedEntity;
    }
    public void AddRange(IEnumerable<TEntity> entities)
        => _dbSet.AddRange(entities);
    public void AddRange(params TEntity[] entities)
        => _dbSet.AddRange(entities);
    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        => await _dbSet.AddRangeAsync(entities, cancellationToken);
    public async Task AddRangeAsync(params TEntity[] entities)
        => await _dbSet.AddRangeAsync(entities, default);
    public TEntity Update(TEntity entity)
        => _dbSet.Update(entity).Entity;
    public async ValueTask<TEntity> UpdateAndSaveChangeAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var updatedEntity = Update(entity);

        await SaveChangesAsync(cancellationToken);

        return updatedEntity;
    }
    public void Delete(TEntity entity)
        => _dbSet.Remove(entity);
    public async Task DeleteAndSaveChangeAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Delete(entity);

        await SaveChangesAsync(cancellationToken);
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _dbContext.SaveChangesAsync(cancellationToken);

    public async ValueTask<IEnumerable<TEntity>> GetAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default)
        => await _dbSet.Specify(specification).ToListAsync(cancellationToken);
}