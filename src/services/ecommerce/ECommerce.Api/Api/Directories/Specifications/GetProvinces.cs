using System.Linq.Expressions;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Directories.Specifications;

public class GetProvincesSpecification : Specification<Province>
{
    public GetProvincesSpecification
    (
        string? keyword,
        PagingQuery? pagingQuery = null
    )
    {
        if (!string.IsNullOrEmpty(keyword))
        {
            var keywordStr = keyword.Trim().ToLower();

            Builder.Where(p => EF.Functions.ILike(EF.Functions.Unaccent(p.Name.Trim().ToLower()), EF.Functions.Unaccent($"%{keywordStr}%"))
                || EF.Functions.ILike(p.Code.Trim().ToLower(), $"%{keywordStr}%")
            );

        }

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}

public class GetProvincesSpecification<TResult> : Specification<Province, TResult>
{
    public GetProvincesSpecification
    (
        Expression<Func<Province, TResult>> selector,
        string? keyword,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        if (keyword != null && keyword.Length > 0)
        {
            Builder.Where(x => x.Name.Contains(keyword) || x.Code.Contains(keyword));
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            var keywordStr = keyword.Trim().ToLower();

            Builder.Where(p => EF.Functions.ILike(EF.Functions.Unaccent(p.Name.Trim().ToLower()), EF.Functions.Unaccent($"%{keywordStr}%"))
                || EF.Functions.ILike(p.Code.Trim().ToLower(), $"%{keywordStr}%")
            );

        }

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}