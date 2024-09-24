namespace ECommerce.Shared.Common.Infrastructure.Settings;

public record CertSettings
{
    public string Ssl { get; init; } = string.Empty;
    public string DataProtection { get; init; } = string.Empty;
    public string EncryptSigning { get; init; } = string.Empty;
}
