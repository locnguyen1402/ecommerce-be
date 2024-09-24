namespace ECommerce.Shared.Common.Infrastructure.Settings;

public record CorsPolicySettings
{
    public string Origins { get; init; } = default!;

    public string[] GetOrigins()
    {
        return Origins.Split(",");
    }
}
