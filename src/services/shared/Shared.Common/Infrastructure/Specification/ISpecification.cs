using System.Linq.Expressions;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Shared.Common.Infrastructure.Specification;

public interface ISpecification
{
    List<string> IncludeStrings { get; }
    IPagingParams? PagingParams { get; }
    bool EnableTracking { get; }
    bool EnableSplitQuery { get; }
}

public interface ISpecification<TEntity> : ISpecification where TEntity : class
{
    Expression<Func<TEntity, bool>>? Criteria { get; }
    List<Expression<Func<TEntity, object?>>> Includes { get; }
}

public interface ISpecification<TEntity, TResult> : ISpecification<TEntity> where TEntity : class
{
    Expression<Func<TEntity, TResult>> Selector { get; }
}