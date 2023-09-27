namespace ECommerce.Shared.Integration.AggregatesModels;
public class OLViewApiJsonSearchResult
{
    [JsonPropertyName("bib_key")]
    public string? BidKey { get; set; }
    [JsonPropertyName("info_url")]
    public string? InfoUrl { get; set; }
    [JsonPropertyName("preview_url")]
    public string? PreviewUrl { get; set; }
    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }
    [JsonPropertyName("preview")]
    public string Preview { get; set; } = string.Empty;
}

public class OLDataJsonSearchResult
{
    [JsonPropertyName("bib_key")]
    public string? BidKey { get; set; }
    [JsonPropertyName("info_url")]
    public string? InfoUrl { get; set; }
    [JsonPropertyName("preview_url")]
    public string? PreviewUrl { get; set; }
    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }
    [JsonPropertyName("preview")]
    public string Preview { get; set; } = string.Empty;
}
