namespace ECommerce.Shared.Integration.RestClients;
public interface IBookRestClient
{
    ValueTask<Book?> GetBookDetail(string id);
}