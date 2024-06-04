using System.Linq.Expressions;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Shared.Common.Infrastructure.Specification;

public class SpecificationBuilder<TEntity>(Specification<TEntity> specification) : ISpecificationBuilder<TEntity>
    where TEntity : class
{
    private readonly Specification<TEntity> _specification = specification;

    public SpecificationBuilder<TEntity> Include(Expression<Func<TEntity, object?>> includeExpression)
    {
        _specification.AddInclude(includeExpression);

        return this;
    }

    public SpecificationBuilder<TEntity> Include(string includeString)
    {
        _specification.AddInclude(includeString);

        return this;
    }

    public SpecificationBuilder<TEntity> Where(Expression<Func<TEntity, bool>> criteria)
    {
        _specification.ApplyCriteria(criteria);

        return this;
    }

    public SpecificationBuilder<TEntity> AsNoTracking()
    {
        _specification.AsNoTracking();

        return this;
    }

    public SpecificationBuilder<TEntity> AsSplitQuery()
    {
        _specification.AsSplitQuery();

        return this;
    }

    public SpecificationBuilder<TEntity> Paginate(IPagingParams pagingParams)
    {
        _specification.ApplyPaging(pagingParams);

        return this;
    }

    public SpecificationBuilder<TEntity> Paginate(int pageIndex, int pageSize)
    {
        _specification.ApplyPaging(pageIndex, pageSize);

        return this;
    }

}