namespace ECommerce.Shared.Common.Infrastructure.Data;

public interface IPagingParams
{
    int PageIndex { get; }
    int PageSize { get; }
    bool FullPagingInfo { get; }
}