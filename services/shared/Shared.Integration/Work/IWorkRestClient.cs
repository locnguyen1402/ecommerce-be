namespace ECommerce.Shared.Integration.RestClients;
public interface IWorkRestClient
{
    ValueTask<List<SearchResultItem>> GetWorks(WorkListQuery query);
    ValueTask<Work?> GetWorkDetail(string id);
}