namespace ECommerce.Shared.Common.Domain.AggregatesModels;
public class PaginationData
{
    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public int TotalItems { get; private set; }
    public int TotalPages { get; private set; }
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
    public PaginationData(int page, int pageSize, int totalItems)
    {
        Page = page;
        PageSize = pageSize;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }
    public string ToPaginationString()
    {
        return JsonSerializer.Serialize(this, JsonConstant.JsonSerializerOptions);
    }
}