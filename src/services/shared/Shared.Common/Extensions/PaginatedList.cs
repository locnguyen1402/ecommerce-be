using ECommerce.Shared.Common.Infrastructure.Data;
using ECommerce.Shared.Common.Pagination;

namespace ECommerce.Shared.Common.Extensions;

public static class PaginatedListExtensions
{
    public static IPaginatedList<T> ToPaginatedList<T>(this IEnumerable<T> source, int page, int pageSize)
    {
        return PaginatedList<T>.Create(source, page, pageSize);
    }
    public static IPaginatedList<T> ToPaginatedList<T>(this IEnumerable<T> source, IPagingParams pagingParams)
    {
        return PaginatedList<T>.Create(source, pagingParams);
    }
    public static Task<IPaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        return PaginatedList<T>.CreateAsync(source, page, pageSize, cancellationToken);
    }
    public static Task<IPaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, IPagingParams pagingParams, CancellationToken cancellationToken = default)
    {
        return PaginatedList<T>.CreateAsync(source, pagingParams, cancellationToken);
    }
}