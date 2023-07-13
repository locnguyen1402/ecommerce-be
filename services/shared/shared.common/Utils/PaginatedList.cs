using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Shared.Common;

public class PaginatedList
{
    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public int TotalItems { get; private set; }
    public PaginatedList(int page, int pageSize, int totalItems)
    {
        Page = page;
        PageSize = pageSize;

        TotalItems = totalItems;
    }

    public string ToPaginationString()
    {
        return JsonSerializer.Serialize(this, JsonConstant.jsonSerializerOptions);
    }

    public static async Task AttachToHeader<TEntity>(IQueryable<TEntity> query, int page, int pageSize)
    {
        var httpContext = new HttpContextAccessor().HttpContext;

        var totalItems = await query.CountAsync();

        PaginatedList paginatedList = new(page, pageSize, totalItems);

        httpContext?.Response.Headers.Add("X-Pagination", paginatedList.ToPaginationString());
    }

    public static async Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> query, int page, int pageSize)
    {
        var items = await query.Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

        return items;
    }
}