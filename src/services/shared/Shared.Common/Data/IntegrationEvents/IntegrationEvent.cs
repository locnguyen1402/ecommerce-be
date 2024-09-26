namespace ECommerce.Shared.Common.Data.IntegrationEvents;

public record IntegrationEvent
{
    public string CorrelationId { get; private set; }
    public DateTimeOffset Timestamp { get; private set; }

    public IntegrationEvent()
    {
        CorrelationId = Guid.NewGuid().ToString();
        Timestamp = DateTimeOffset.UtcNow;
    }
}
