namespace ECommerce.Shared.Libs.Extensions;
public static class QueryableExtensions
{
    public static async Task<List<T>> ToPaginatedListAsync<T>(this IQueryable<T> query, int page, int pageSize)
    {
        return await query.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    }

}