namespace ECommerce.Shared.Integration.RestClients;
public interface IWorkRestClient
{
    ValueTask<PaginatedList<SearchResultItem>> GetWorks(WorkListQuery query);
    ValueTask<Work?> GetWorkDetail(string id);
}