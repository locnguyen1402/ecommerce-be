namespace ECommerce.Shared.Common.AppSettings;
public class Integration
{
    public OpenLibrarySettings OpenLibrary { get; init; } = new();
    public record OpenLibrarySettings : Shared
    {
    }
    public record Shared
    {
        public RestClientsSettings RestClients { get; set; } = new();
        public record RestClientsSettings
        {
            public string BaseUrl { get; init; } = string.Empty;
        }
    }
}