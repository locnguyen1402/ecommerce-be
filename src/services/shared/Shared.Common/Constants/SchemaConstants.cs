namespace ECommerce.Shared.Common.Constants;

public sealed partial class SchemaConstants
{
    private SchemaConstants() { }

    public const string IDENTITY_TABLE_PREFIX = "AspNet";
    public const string IDENTITY_API_TABLE_PREFIX = "OpenIddict";

    public const string DATABASE_CONNECTION = "DatabaseConnection";
    public const string AUDIT_EVENT_CONNECTION = "AuditEventConnection";
    public const string MESSAGE_BUS_CONNECTION = "MessageBusConnection";

    public const string APP_DB_CONNECTION = "AppDb";
    public const string EVENT_BUS_DB_CONNECTION = "EventBusDb";
    public const string EVENT_BUS_CONNECTION = "EventBus";
    public const string DISTRIBUTED_CACHING_CONNECTION = "DistributedCaching";

    public const string EXTRA_PROPERTIES = "ExtraProperties";

    public const double COMMAND_TIMEOUT = 30;
    public const int MAX_RETRY_COUNT = 3;
    public const short MAX_STRING_COLUMN_LENGTH = 2000;
}
