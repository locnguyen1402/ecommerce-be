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

    public async ValueTask<PaginatedList<SearchResultItem>> GetWorks(WorkListQuery query)
    {
        var page = query.Page - 1;
        var queryString = $"q={query.Keyword}&offset={page * query.PageSize}&limit={query.PageSize}";

        var restRequest = new RestRequest($"/search.json?{queryString}&mode=ebooks&has_fulltext=true");

        var response = await _client.ExecuteGetAsync<OLWorkListResponse>(restRequest);

        if (response.Data == null)
        {
            return PaginatedList<SearchResultItem>.CreateEmpty(query.Page, query.PageSize);
        }

        var mappedVal = _mapper.Map<List<OLSearchResultItem>, List<SearchResultItem>>(response.Data.Docs);

        return new PaginatedList<SearchResultItem>(mappedVal, query.Page, query.PageSize, response.Data.TotalItems);
    }

    public async ValueTask<Work?> GetWorkDetail(string id)
    {
        var restRequest = new RestRequest($"/works/{id}.json");

        var response = await _client.ExecuteGetAsync<OLWork>(restRequest);

        if (response.Data == null || !response.Data.Error.IsNullOrEmpty())
        {
            return null;
        }

        var mappedVal = _mapper.Map<OLWork, Work>(response.Data);

        return mappedVal;
    }

    public async ValueTask<PaginatedList<Book>> GetBooksInWork(string workId, PaginationQuery query)
    {
        var page = query.Page - 1;
        var queryString = $"offset={page * query.PageSize}&limit={query.PageSize}";

        var restRequest = new RestRequest($"/works/{workId}/editions.json?{queryString}");
        var response = await _client.ExecuteGetAsync<OLInWorkBookListResponse>(restRequest);

        if (response.Data == null || response.Data.Entries.IsNullOrEmpty())
        {
            return PaginatedList<Book>.CreateEmpty(query.Page, query.PageSize);
        }

        var mappedVal = _mapper.Map<List<OLBook>, List<Book>>(response.Data.Entries);

        return new PaginatedList<Book>(mappedVal, query.Page, query.PageSize, response.Data.Size);
    }
}