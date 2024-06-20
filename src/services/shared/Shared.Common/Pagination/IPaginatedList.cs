namespace ECommerce.Shared.Common.Pagination;

public interface IPaginatedList
{
    int PageIndex { get; }
    int PageSize { get; }
    bool HasPreviousPage { get; }
    bool HasNextPage { get; }
}

public interface IPaginatedList<out T> : IPaginatedList, IReadOnlyList<T>
{
    string ToJsonString();
    void PopulatePaginationInfo();
}