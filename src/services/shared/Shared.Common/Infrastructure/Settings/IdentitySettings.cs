namespace ECommerce.Shared.Common.Infrastructure.Settings;

public class IdentitySettings : IIdentitySettings
{
    public string Authority { get; set; } = string.Empty;
    public string[] Audiences { get; set; } = [];
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
}
