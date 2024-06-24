using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Shared.Common.Queries;

public class PagingQuery
{
    private const int DefaultPageIndex = 1;
    private const int DefaultPageSize = 10;
    private const bool DefaultFullPagingInfo = false;
    private const string pageIndexKey = "pageIndex";
    private const string pageSizeKey = "pageSize";
    private const string fullPagingInfoKey = "fullPagingInfo";
    public int PageIndex { get; init; } = DefaultPageIndex;
    public int PageSize { get; init; } = DefaultPageSize;
    public bool FullPagingInfo { get; init; } = DefaultFullPagingInfo;

    private static int GetPageIndex(HttpContext context)
    {
        if (int.TryParse(context.Request.Query[pageIndexKey], out var pageIndex))
        {
            return pageIndex > 0 ? pageIndex : DefaultPageIndex;
        }
        else
        {
            return DefaultPageIndex;
        }
    }

    private static int GetPageSize(HttpContext context)
    {
        if (int.TryParse(context.Request.Query[pageSizeKey], out var pageSize))
        {
            return pageSize > 0 ? pageSize : DefaultPageSize;
        }
        else
        {
            return DefaultPageSize;
        }
    }

    private static bool HasFullPagingInfo(HttpContext context)
    {
        if (bool.TryParse(context.Request.Query[fullPagingInfoKey], out var fullPagingInfo))
        {
            return fullPagingInfo;
        }
        else
        {
            return DefaultFullPagingInfo;
        }
    }

    public static ValueTask<PagingQuery?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        var result = new PagingQuery
        {
            PageIndex = GetPageIndex(context),
            PageSize = GetPageSize(context),
            FullPagingInfo = HasFullPagingInfo(context)
        };

        return ValueTask.FromResult<PagingQuery?>(result);
    }
}