using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Shared.Common;

public interface IEntityRepository<TEntity> where TEntity : Entity
{
    IQueryable<TEntity> Query { get; }
    ValueTask<TEntity?> FindAsync(object[] keyValues);
    ValueTask<TEntity?> FindAsync(object keyValues);
    ValueTask<TEntity> AddAsync(TEntity entity);
    ValueTask<TEntity> UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task<int> SaveChangesAsync();
}