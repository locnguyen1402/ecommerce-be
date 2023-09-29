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
        var queryList = new List<string>{
            "mode=ebooks",
            "has_fulltext=true"
        };

        if (!query.Title.IsNullOrEmpty())
        {
            queryList.Add($"title={query.Title}");
        }
        if (!query.Author.IsNullOrEmpty())
        {
            queryList.Add($"author={query.Author}");
        }
        if (!query.Place.IsNullOrEmpty())
        {
            queryList.Add($"place={query.Place}");
        }
        if (!query.Subject.IsNullOrEmpty())
        {
            queryList.Add($"subject={query.Subject}");
        }
        if (!query.Person.IsNullOrEmpty())
        {
            queryList.Add($"person={query.Person}");
        }
        if (!query.Keyword.IsNullOrEmpty())
        {
            queryList.Add($"q={query.Keyword}");
        }
        if (query.HasFullText == true)
        {
            queryList.Add("has_fulltext=true");
        }

        if (query.Recover == true)
        {
            queryList.Add($"offset=0&limit={query.Page * query.PageSize}");
        }
        else
        {
            queryList.Add($"offset={page * query.PageSize}&limit={query.PageSize}");
        }

        var url = $"/search.json?{string.Join("&", queryList)}";
        var restRequest = new RestRequest(url);

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

    public async ValueTask<Book?> GetFirstInWorkBook(string workId)
    {
        var response = await GetBooksInWork(workId, new PaginationQuery { Page = 1, PageSize = 1 });

        if (response.Items.IsNullOrEmpty())
        {
            return null;
        }

        return response.Items.First();
    }

    public async ValueTask<WorkRatings?> GetWorkRatings(string workId)
    {
        var restRequest = new RestRequest($"/works/{workId}/ratings.json");
        var response = await _client.ExecuteGetAsync<OLWorkRatings>(restRequest);

        if (response.Data == null)
        {
            return null;
        }

        var mappedVal = _mapper.Map<OLWorkRatings, WorkRatings>(response.Data);

        return mappedVal;
    }
}