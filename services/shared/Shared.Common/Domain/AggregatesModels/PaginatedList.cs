namespace ECommerce.Shared.Common.Domain.AggregatesModels;
public class PaginatedList<T>
{
    public List<T> Items { get; private set; }
    public PaginationData PaginationData { get; private set; }
    public PaginatedList(List<T> items, PaginationData paginationData)
    {
        Items = items;
        PaginationData = paginationData;
    }
    public PaginatedList(List<T> items, int page, int pageSize, int totalItems) :
        this(items, new PaginationData(page, pageSize, totalItems))
    {
    }
    public static PaginatedList<T> CreateEmpty(int? page, int? pageSize)
    {
        return new PaginatedList<T>(new List<T>(), page ?? 0, pageSize ?? 0, 0);
    }

    public static async Task<PaginatedList<T>> CreateFromQuery(IQueryable<T> query, int page, int pageSize)
    {
        var totalItems = await query.CountAsync();
        var items = await query.ToPaginatedListAsync(page, pageSize);

        return new PaginatedList<T>(items, page, pageSize, totalItems);
    }

    public static void AttachToHeader(PaginationData paginationData)
    {
        var httpContext = new HttpContextAccessor().HttpContext;

        httpContext?.Response.Headers.Add("X-Pagination", paginationData.ToPaginationString());
    }

    public void ExposeHeader()
    {
        AttachToHeader(this.PaginationData);
    }
}