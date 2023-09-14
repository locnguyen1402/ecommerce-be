using System.Text.Json.Serialization;

namespace ECommerce.Shared.Integration.AggregatesModels;
// public class BookStatus
// {
//     private BookStatus(string value) { Value = value; }
//     public string Value { get; private set; }

//     public static BookStatus Trace { get { return new BookStatus("borrow"); } }
//     public static BookStatus Debug { get { return new BookStatus("full"); } }
//     public static BookStatus Info { get { return new BookStatus("restricted"); } }
//     public static BookStatus Warning { get { return new BookStatus("noview"); } }

//     public override string ToString()
//     {
//         return Value;
//     }
// }
public class OLJsonSearchResult
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
