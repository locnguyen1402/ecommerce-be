using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Common.Mediator;

/// <summary>
/// Publishes an Event to all registered handlers.
/// </summary>
/// <param name="serviceProvider"></param>
public class EventPublisher(IServiceProvider serviceProvider) : IEventPublisher
{
    /// <summary>
    /// Publishes an Event to all registered handlers.
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    /// <param name="event"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IEvent
    {
        // TODO: Handler Pre Processing

        var handlers = serviceProvider.GetServices<IEventHandler<TEvent>>();

        var tasks = handlers.Select(handler => handler.HandleAsync(@event, cancellationToken));

        await Task.WhenAll(tasks);

        // TODO: Handler Post Processing
    }
}
