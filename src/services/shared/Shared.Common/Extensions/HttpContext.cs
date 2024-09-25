using ECommerce.Shared.Common.Constants;
using ECommerce.Shared.Common.Pagination;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Shared.Common.Extensions;

public static class HttpContextExtensions
{
    public static void AddPaginationInfo<T>(this HttpContext httpContext, IPaginatedList<T> source)
    {
        httpContext.Response.Headers.Append(HeaderConstants.PAGINATION_KEY, source.ToJsonString());
        httpContext.Response.Headers.Append(HeaderConstants.ACCESS_CONTROL_EXPOSE_HEADERS, HeaderConstants.PAGINATION_KEY);
    }

    public static void SetContentDispositionResponseHeader(this HttpContext httpContext)
    {
        httpContext.Response.Headers.Append(HeaderConstants.ACCESS_CONTROL_EXPOSE_HEADERS, HeaderConstants.CONTENT_DISPOSITION);
    }
}