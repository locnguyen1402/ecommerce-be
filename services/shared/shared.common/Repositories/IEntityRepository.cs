using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Shared.Common;

public interface IEntityRepository<TEntity> where TEntity : Entity
{
    IQueryable<TEntity> Query { get; }
    ValueTask<TEntity?> FindAsync(object[] keyValues);
    ValueTask<TEntity?> FindAsync(object keyValues);
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    void Remove(TEntity entity);
    Task<int> SaveChangesAsync();
}