namespace ECommerce.Shared.Integration.RestClients;
public class WorkRestClient : IWorkRestClient
{
    private readonly RestClient _client;
    private readonly IMapper _mapper;
    public WorkRestClient(string baseUrl, IMapper mapper)
    {
        _client = new RestClient(baseUrl);
        _mapper = mapper;
    }

    public async ValueTask<List<Work>> GetWorks(WorkListQuery query)
    {
        var restRequest = new RestRequest($"/search?q={query.Query}");

        var response = await _client.ExecuteGetAsync<OLWorkListResponse>(restRequest);

        if (response.Data == null)
        {
            return new List<Work>();
        }

        var mappedVal = _mapper.Map<List<OLWorkItemResponse>, List<Work>>(response.Data.Docs);

        return mappedVal;
    }

    public async ValueTask<Work?> GetWorkDetail(string id)
    {
        var restRequest = new RestRequest($"/books/{id}.json");

        var response = await _client.ExecuteGetAsync<OLWorkResponse>(restRequest);

        if (response.Data == null)
        {
            return null;
        }

        var mappedVal = _mapper.Map<OLWorkResponse, Work>(response.Data);

        return mappedVal;
    }


}