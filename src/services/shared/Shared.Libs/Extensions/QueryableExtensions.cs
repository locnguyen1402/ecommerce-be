// namespace ECommerce.Shared.Libs.Extensions;
// public static class QueryableExtensions
// {
//     public static async Task<List<T>> ToPaginatedListAsync<T>(this IQueryable<T> query, int page, int pageSize)
//     {
//         return await query.Skip((page - 1) * pageSize)
//                     .Take(pageSize)
//                     .ToListAsync();
//     }

//     public static IList<T> ToPaginatedList<T>(this IList<T> list, int page, int pageSize)
//     {
//         return list.Skip((page - 1) * pageSize)
//                     .Take(pageSize)
//                     .ToList();
//     }
// }