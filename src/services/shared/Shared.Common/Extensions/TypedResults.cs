using ECommerce.Shared.Common.Pagination;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Shared.Common.Extensions;

public static class TypedResultsExtensions
{
    public static IResult PaginatedListOk<T>(this IResultExtensions _, IPaginatedList<T> paginatedList)
    {
        return new PaginatedResult<T>(paginatedList);
    }
}