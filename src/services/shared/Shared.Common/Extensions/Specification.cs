using ECommerce.Shared.Common.Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Shared.Common.Extensions;

public static class SpecificationExtensions
{
    private static IQueryable<TEntity> BaseSpecify<TEntity>(this IQueryable<TEntity> query, ISpecification specification) where TEntity : class
    {
        query = specification.IncludeStrings
            .Aggregate(query, (current, include) => current.Include(include));

        if (!specification.EnableTracking)
        {
            query = query.AsNoTracking();
        }

        if (specification.EnableSplitQuery)
        {
            query = query.AsSplitQuery();
        }

        return query;
    }
    public static IQueryable<TEntity> Specify<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        query = query.BaseSpecify(specification);

        query = specification.Includes
            .Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
    public static IQueryable<TResult> Specify<TEntity, TResult>(this IQueryable<TEntity> query, ISpecification<TEntity, TResult> specification) where TEntity : class
    {
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        query = query.BaseSpecify(specification);

        query = specification.Includes
            .Aggregate(query, (current, include) => current.Include(include));

        return query.Select(specification.Selector);
    }
}