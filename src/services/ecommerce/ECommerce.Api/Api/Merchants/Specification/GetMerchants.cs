using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Helper;
using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Merchants.Specifications;

public class GetMerchantsSpecification : Specification<Merchant>
{
    public GetMerchantsSpecification
    (
        string? keyword,
        PagingQuery? pagingQuery = null
    )
    {
        if (keyword != null && keyword.Length > 0)
        {
            Builder.Where(x => x.Name.Contains(keyword));
        }

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}

public class GetMerchantsSpecification<TResult> : Specification<Merchant, TResult>
{
    public GetMerchantsSpecification
    (
        Expression<Func<Merchant, TResult>> selector,
        string? keyword,
        bool? hasStores,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        Builder.Where(BuildCriteria(keyword, hasStores));

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }


    private static Expression<Func<Merchant, bool>> BuildCriteria(string? keyword, bool? hasStores)
    {
        Expression<Func<Merchant, bool>> criteria = p => true;

        if (!string.IsNullOrEmpty(keyword))
            criteria = criteria.And(p => EF.Functions.ILike(EF.Functions.Unaccent(p.Name), EF.Functions.Unaccent($"%{keyword}%")));


        if (hasStores.HasValue && hasStores.Value)
            criteria = criteria.And(p => p.Stores.Count != 0);

        return criteria;
    }
}