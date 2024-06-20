using Microsoft.AspNetCore.Http;

using ECommerce.Shared.Common.Extensions;

namespace ECommerce.Shared.Common.Pagination;

public class PaginatedResult<T>(IPaginatedList<T> source) : IResult
{
    private readonly IPaginatedList<T> _paginatedList = source;
    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.AddPaginationInfo(_paginatedList);

        return httpContext.Response.WriteAsJsonAsync(_paginatedList);
    }
}