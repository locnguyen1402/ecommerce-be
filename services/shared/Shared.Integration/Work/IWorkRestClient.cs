namespace ECommerce.Shared.Integration.RestClients;
public interface IWorkRestClient
{
    ValueTask<List<Work>> GetWorks(WorkListQuery query);
    ValueTask<Work?> GetWorkDetail(string id);
}