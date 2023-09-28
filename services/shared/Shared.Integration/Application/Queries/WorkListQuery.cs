namespace ECommerce.Shared.Integration.Application.Queries;
public class WorkListQuery : PaginationQuery
{
    public string? Keyword { get; set; }
    public bool? Recover { get; set; }
}