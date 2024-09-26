using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Helper;
using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Customers.Specifications;

public class GetCustomersSpecification : Specification<Customer>
{
    public GetCustomersSpecification
    (
        string? keyword,
        PagingQuery? pagingQuery = null
    )
    {
        Builder.Where(BuildCriteria(keyword));

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }

    public static Expression<Func<Customer, bool>> BuildCriteria(string? keyword)
    {
        Expression<Func<Customer, bool>> criteria = p => true;

        if (!string.IsNullOrEmpty(keyword))
        {
            criteria = criteria.And(p => EF.Functions.ILike(EF.Functions.Unaccent(p.FullName.Trim().ToLower()), EF.Functions.Unaccent($"%{keyword.Trim().ToLower()}%"))
                || (p.Email != null && EF.Functions.ILike(EF.Functions.Unaccent(p.Email.Trim().ToLower()), EF.Functions.Unaccent($"%{keyword.Trim().ToLower()}%")))
                || (p.PhoneNumber != null && EF.Functions.ILike(p.PhoneNumber, $"%{keyword}%"))
            );
        }

        return criteria;
    }
}

public class GetCustomersSpecification<TResult> : Specification<Customer, TResult>
{
    public GetCustomersSpecification
    (
        Expression<Func<Customer, TResult>> selector,
        string? keyword,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        Builder.Where(GetCustomersSpecification.BuildCriteria(keyword));

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}