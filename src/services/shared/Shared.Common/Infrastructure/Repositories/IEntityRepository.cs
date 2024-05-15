namespace ECommerce.Shared.Common.Infrastructure.Repositories;
public interface IEntityRepository<TEntity> where TEntity : Entity
{
    IQueryable<TEntity> Query { get; }
    ValueTask<bool> IsExisted(Guid id);
    ValueTask<TEntity?> FindAsync(object[] keyValues);
    ValueTask<TEntity?> FindAsync(object keyValues);
    EntityEntry<TEntity> Attach(TEntity entity);
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    void Remove(TEntity entity);
    Task<int> SaveChangesAsync();
    EntityEntry<TEntity> CreateEntry(TEntity entity);
}