namespace ECommerce.Shared.Common.AppSettings;
public class Integration
{
    public record Shared
    {
        public RestClientsSettings RestClients { get; set; } = new();
        public record RestClientsSettings
        {
            public string BaseUrl { get; init; } = string.Empty;
        }
    }
}