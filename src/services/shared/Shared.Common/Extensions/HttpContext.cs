using ECommerce.Shared.Common.Constants;
using ECommerce.Shared.Common.Pagination;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Shared.Common.Extensions;

public static class HttpContextExtensions
{
    public static void AddPaginationInfo<T>(this HttpContext httpContext, IPaginatedList<T> source)
    {
        httpContext.Response.Headers.Append(HeaderConstants.PAGINATION_KEY, source.ToJsonString());
    }
}