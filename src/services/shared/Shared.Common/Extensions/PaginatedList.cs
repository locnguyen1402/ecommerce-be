using ECommerce.Shared.Common.Pagination;

namespace ECommerce.Shared.Common.Extensions;

public static class PaginatedListExtensions
{
    public static Task<IPaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        return PaginatedList<T>.CreateAsync(source, page, pageSize, cancellationToken);
    }
}