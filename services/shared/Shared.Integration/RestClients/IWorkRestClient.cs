namespace ECommerce.Shared.Integration.RestClients;
public interface IWorkRestClient
{
    ValueTask<PaginatedList<SearchResultItem>> GetWorks(WorkListQuery query);
    ValueTask<PaginatedList<SearchResultItem>> GetTrendingWorks(TrendingWorksQuery query);
    ValueTask<Work?> GetWorkDetail(string id);
    ValueTask<PaginatedList<Book>> GetBooksInWork(string workId, PaginationQuery query);
    ValueTask<Book?> GetFirstInWorkBook(string workId);
    ValueTask<WorkRatings?> GetWorkRatings(string workId);
}