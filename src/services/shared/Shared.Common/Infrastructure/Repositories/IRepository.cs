using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Shared.Common.Infrastructure.Repositories;
public interface IRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
{
    ValueTask<bool> IsExisted(Guid id);
}