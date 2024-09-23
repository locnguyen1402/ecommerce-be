namespace ECommerce.Shared.Infrastructure.Settings;

public interface IBaseAppSettings
{
    string PathBase { get; }
    CorsPolicySettings CorsPolicy { get; }
    CertSettings Certs { get; }

    string AppInstance { get; }
    string DistributedCacheInstance { get; }
}
