using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Shared.Integration.RestClients;
public class BookRestClient : IBookRestClient
{
    private readonly RestClient _client;
    private readonly IMapper _mapper;
    public BookRestClient(string baseUrl, IMapper mapper)
    {
        _client = new RestClient(baseUrl);
        _mapper = mapper;
    }

    public async ValueTask<Book?> GetBookDetail(string id)
    {
        var restRequest = new RestRequest($"/books/{id}.json");

        var response = await _client.ExecuteGetAsync<OLBook>(restRequest);
        var previewData = await GetPreviewDataBook(id);

        if (response.Data is null || previewData is null)
        {
            return null;
        }

        var mappedVal = _mapper.Map<OLBook, Book>(response.Data);

        if (Enum.TryParse(previewData.Preview, true, out BookStatus val))
        {
            mappedVal.UpdateStatus(val);
        }

        return mappedVal;
    }

    private async ValueTask<OLJsonSearchResult?> GetPreviewDataBook(string id)
    {
        var jsonListData = await GetPreviewDataBooks(new string[] { id });

        if (jsonListData.IsNullOrEmpty())
        {
            return null;
        }

        return jsonListData!.First();
    }

    private async ValueTask<List<OLJsonSearchResult>?> GetPreviewDataBooks(string[] bookIds)
    {
        var bibKeys = new List<string>();

        foreach (var bookId in bookIds)
        {
            bibKeys.Add($"OLID:{bookId}");
        }

        var queryString = $"bibkeys={String.Join(",", bibKeys.ToArray())}&format=json";

        var restRequest = new RestRequest($"/api/books?{queryString}");

        var response = await _client.ExecuteGetAsync<dynamic>(restRequest);

        if (response.Data is null)
        {
            return null;
        }

        try
        {
            var result = new List<OLJsonSearchResult>();
            var jsonData = JsonSerializer.Deserialize<Dictionary<string, OLJsonSearchResult>>(response.Data, JsonConstant.JsonSerializerOptions);
            foreach (KeyValuePair<string, OLJsonSearchResult> entry in jsonData)
            {
                result.Add(entry.Value);
            }

            return result;

        }
        catch (Exception err)
        {
            Console.WriteLine(err.Message);
            return null;
        }
    }
}