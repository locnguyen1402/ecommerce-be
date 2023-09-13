using System.ComponentModel;

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
}