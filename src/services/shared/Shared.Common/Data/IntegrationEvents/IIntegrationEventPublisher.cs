namespace ECommerce.Shared.Common.Data.IntegrationEvents;

public interface IIntegrationEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IntegrationEvent;
}
