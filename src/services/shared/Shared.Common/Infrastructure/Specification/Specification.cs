using System.Linq.Expressions;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Shared.Common.Infrastructure.Specification;

public class Specification<TEntity> : ISpecification<TEntity> where TEntity : class
{
    protected ISpecificationBuilder<TEntity> Builder { get; }
    public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
    public List<Expression<Func<TEntity, object?>>> Includes { get; } = [];
    public List<string> IncludeStrings { get; } = [];
    public IPagingParams? PagingParams { get; private set; }
    public bool EnableTracking { get; private set; } = true;
    public bool EnableSplitQuery { get; private set; } = false;
    public Specification()
    {
        Builder = new SpecificationBuilder<TEntity>(this);
    }
    public void ApplyCriteria(Expression<Func<TEntity, bool>> criteria)
        => Criteria = criteria;
    public void AddInclude(Expression<Func<TEntity, object?>> includeExpression)
        => Includes.Add(includeExpression);
    public void AddInclude(string includeString)
        => IncludeStrings.Add(includeString);
    public void ApplyPaging(IPagingParams pagingParams)
        => PagingParams = pagingParams;
    public void ApplyPaging(int pageIndex, int pageSize, bool fullPagingInfo)
        => PagingParams = new PagingParams(pageIndex, pageSize, fullPagingInfo);
    public void AsNoTracking()
        => EnableTracking = false;
    public void AsSplitQuery()
        => EnableSplitQuery = true;
}

public class Specification<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector) : Specification<TEntity>, ISpecification<TEntity, TResult> where TEntity : class
{
    public Expression<Func<TEntity, TResult>> Selector { get; private set; } = selector;
}
