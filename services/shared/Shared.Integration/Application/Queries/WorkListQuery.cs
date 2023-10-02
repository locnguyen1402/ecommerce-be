namespace ECommerce.Shared.Integration.Application.Queries;
public class WorkListQuery : PaginationQuery
{
    public string? Keyword { get; set; }
    public bool? Recover { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Subject { get; set; }
    public string? Place { get; set; }
    public string? Person { get; set; }
    public bool? HasFullText { get; set; }
}