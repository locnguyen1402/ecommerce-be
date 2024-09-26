using MassTransit;

using ECommerce.Shared.Common.Data.IntegrationEvents;

namespace ECommerce.Shared.Common.EventBus.Abstractions;

public abstract class IntegrationEventHandler<TEvent> : IConsumer<TEvent>, IIntegrationEventHandler<TEvent>
    where TEvent : IntegrationEvent
{
    public async Task Consume(ConsumeContext<TEvent> context)
    {
        await HandleAsync(context.Message, context.CancellationToken);
    }

    public virtual async Task HandleAsync(ConsumeContext<TEvent> context)
    {
        await HandleAsync(context.Message, context.CancellationToken);
    }

    public virtual Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
