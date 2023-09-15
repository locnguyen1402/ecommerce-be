namespace ECommerce.Shared.Integration.Application.Responses;
public class OLPaginationResponse
{
    [JsonPropertyName("NumFound")]
    public int TotalItems { get; set; }
    public int Offset { get; set; } = 0;
}