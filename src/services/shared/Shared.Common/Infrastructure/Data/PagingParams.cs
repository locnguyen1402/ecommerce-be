namespace ECommerce.Shared.Common.Infrastructure.Data;

public class PagingParams(int pageIndex, int pageSize) : IPagingParams
{
    public int PageIndex { get; init; } = pageIndex;
    public int PageSize { get; init; } = pageSize;
}