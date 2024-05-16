namespace ECommerce.Shared.Common.Infrastructure.Repositories;
public interface IBaseRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Query { get; }
    ValueTask<TEntity?> FindAsync(object[] keyValues);
    ValueTask<TEntity?> FindAsync(object keyValues);
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    void Remove(TEntity entity);
    Task<int> SaveChangesAsync();
    // ValueTask<bool> IsExisted(Guid id);
}