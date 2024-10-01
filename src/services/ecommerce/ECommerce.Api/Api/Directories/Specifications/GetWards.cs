using System.Linq.Expressions;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Helper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Directories.Specifications;

public class GetWardsSpecification : Specification<Ward>
{
    public GetWardsSpecification
    (
        string? keyword,
        Guid districtId,
        PagingQuery? pagingQuery = null
    )
    {
        Builder.Where(BuildCriteria(keyword, districtId));

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }

    public static Expression<Func<Ward, bool>> BuildCriteria(string? keyword, Guid districtId)
    {
        Expression<Func<Ward, bool>> criteria = p => true;

        criteria = criteria.And(p => p.DistrictId == districtId);

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

public class GetWardsSpecification<TResult> : Specification<Ward, TResult>
{
    public GetWardsSpecification
    (
        Expression<Func<Ward, TResult>> selector,
        string? keyword,
        Guid districtId,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        Builder.Where(GetWardsSpecification.BuildCriteria(keyword, districtId));

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}