using System.Linq.Expressions;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Shared.Common.Infrastructure.Specification;

public interface ISpecificationBuilder<TEntity> where TEntity : class
{
    SpecificationBuilder<TEntity> Include(Expression<Func<TEntity, object?>> includeExpression);
    SpecificationBuilder<TEntity> Include(string includeString);
    SpecificationBuilder<TEntity> Where(Expression<Func<TEntity, bool>> criteria);
    SpecificationBuilder<TEntity> AsNoTracking();
    SpecificationBuilder<TEntity> AsSplitQuery();
    SpecificationBuilder<TEntity> Paginate(IPagingParams pagingParams);
    SpecificationBuilder<TEntity> Paginate(int pageIndex, int pageSize, bool fullPagingInfo = false);
}