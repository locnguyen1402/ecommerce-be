namespace ECommerce.Shared.Common.Domain.Queries;
public class PaginationQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}