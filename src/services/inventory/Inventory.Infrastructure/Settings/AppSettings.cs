using ECommerce.Shared.Infrastructure.Settings;

namespace ECommerce.Inventory.Infrastructure.Settings;

public record AppSettings : BaseAppSettings
{
    public IdentitySettings Identity { get; set; } = new();

    public string DistributedCacheHost { get; set; } = string.Empty;

    public ConfigSettings Config { get; set; } = new();
}

public record ConfigSettings
{
    public int AccessTokenExpireMinute { get; set; } = new();
    public int RefreshTokenExpireMinute { get; set; } = new();
}
