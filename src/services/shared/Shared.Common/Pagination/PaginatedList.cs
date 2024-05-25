using Microsoft.EntityFrameworkCore;

namespace ECommerce.Shared.Common.Pagination;

public class PaginatedList<T> : List<T>, IPaginatedList<T>
{
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage { get; private set; } = false;

    public PaginatedList(IEnumerable<T> items, int pageIndex, int pageSize, bool hasNextPage = false)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        HasNextPage = hasNextPage;

        AddRange(items);
    }


    public static async Task<IPaginatedList<T>> CreateAsync(IQueryable<T> source, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize + 1).ToListAsync(cancellationToken);

        var hasNextPage = items.Count > pageSize;
        if (hasNextPage)
        {
            items = items.Take(pageSize).ToList();
        }

        return new PaginatedList<T>(items, page, pageSize, hasNextPage);
    }
}