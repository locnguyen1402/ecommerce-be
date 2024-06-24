namespace ECommerce.Shared.Common.Infrastructure.Data;

public class PagingParams(int pageIndex, int pageSize, bool fullPagingInfo) : IPagingParams
{
    public int PageIndex { get; init; } = pageIndex;
    public int PageSize { get; init; } = pageSize;
    public bool FullPagingInfo { get; init; } = fullPagingInfo;
}