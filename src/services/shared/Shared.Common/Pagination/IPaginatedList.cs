namespace ECommerce.Shared.Common.Pagination;

public interface IPaginatedList<T>
{
    int PageIndex { get; }
    int PageSize { get; }
    bool HasPreviousPage { get; }
    bool HasNextPage { get; }
}