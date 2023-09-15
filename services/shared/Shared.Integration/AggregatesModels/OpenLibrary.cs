namespace ECommerce.Shared.Integration.AggregatesModels;

public enum OLImageSize
{
    [Description("Small")]
    S,
    [Description("Medium")]
    M,
    [Description("Large")]
    L
}

public sealed partial class OLConstants
{
    public const string IMAGE_BASE_URL = "https://covers.openlibrary.org";
    public const string INTERNET_ARCHIVE_URL = "https://archive.org";
    public const string INTERNET_ARCHIVE_IMAGE_BASE_URL = $"{INTERNET_ARCHIVE_URL}/services/img";
}