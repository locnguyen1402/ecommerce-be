using System.Text.Json.Serialization;

namespace ECommerce.Shared.Integration.Application.Responses;
public class OLWorkListResponse : OLPaginationResponse
{
    public List<OLWorkItemResponse> Docs { get; set; } = new List<OLWorkItemResponse>();
}

public class OLWorkItemResponse
{
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    [JsonPropertyName("lending_edition_s")]
    public string LendingEditionS { get; set; } = string.Empty;
    [JsonPropertyName("cover_i")]
    public int? CoverI { get; set; }
}