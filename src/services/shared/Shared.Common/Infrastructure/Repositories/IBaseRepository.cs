using System.Linq.Expressions;
using ECommerce.Shared.Common.Infrastructure.Specification;
using ECommerce.Shared.Common.Pagination;

namespace ECommerce.Shared.Common.Infrastructure.Repositories;
public interface IBaseRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Query { get; }
    ValueTask<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken = default);
    ValueTask<TEntity?> FindAsync(object keyValues, CancellationToken cancellationToken = default);
    ValueTask<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    ValueTask<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    TEntity Add(TEntity entity);
    ValueTask<TEntity> AddAndSaveChangeAsync(TEntity entity, CancellationToken cancellationToken = default);
    void AddRange(IEnumerable<TEntity> entities);
    void AddRange(params TEntity[] entities);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task AddRangeAsync(params TEntity[] entities);
    TEntity Update(TEntity entity);
    ValueTask<TEntity> UpdateAndSaveChangeAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Delete(TEntity entity);
    Task DeleteAndSaveChangeAsync(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<TEntity>> GetAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);
    ValueTask<IPaginatedList<TEntity>> PaginateAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}