namespace ECommerce.Shared.Common.Data.IntegrationEvents;

public interface IIntegrationEventHandler
{
}

public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
{
    Task HandleAsync(TIntegrationEvent @event, CancellationToken cancellationToken = default);
}
