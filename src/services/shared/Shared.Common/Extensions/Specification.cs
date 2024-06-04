using ECommerce.Shared.Common.Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Shared.Common.Extensions;

public static class SpecificationExtensions
{
    public static IQueryable<TEntity> Specify<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification) where TEntity : class
    {
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification.Includes
            .Aggregate(query, (current, include) => current.Include(include));

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
}