using System.Linq.Expressions;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Helper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Api.Directories.Specifications;

public class GetDistrictsSpecification : Specification<District>
{
    public GetDistrictsSpecification
    (
        string? keyword,
        Guid provinceId,
        PagingQuery? pagingQuery = null
    )
    {
        Builder.Where(BuildCriteria(keyword, provinceId));

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }

    public static Expression<Func<District, bool>> BuildCriteria(string? keyword, Guid provinceId)
    {
        Expression<Func<District, bool>> criteria = p => true;

        criteria = criteria.And(p => p.ProvinceId == provinceId);

        if (!string.IsNullOrEmpty(keyword))
        {
            var keywordStr = keyword.Trim().ToLower();

            criteria = criteria.And(p => EF.Functions.ILike(EF.Functions.Unaccent(p.Name.Trim().ToLower()), EF.Functions.Unaccent($"%{keywordStr}%"))
                || EF.Functions.ILike(p.Code.Trim().ToLower(), $"%{keywordStr}%")
            );
        }

        return criteria;
    }
}

public class GetDistrictsSpecification<TResult> : Specification<District, TResult>
{
    public GetDistrictsSpecification
    (
        Expression<Func<District, TResult>> selector,
        string? keyword,
        Guid provinceId,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        Builder.Where(GetDistrictsSpecification.BuildCriteria(keyword, provinceId));

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}