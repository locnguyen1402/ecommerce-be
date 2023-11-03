namespace ECommerce.Shared.Libs.Extensions;
public static class ListExtensions
{
    public static IList<T> ToPaginatedList<T>(this IList<T> list, int page, int pageSize)
    {
        return list.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
    }
}