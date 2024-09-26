using MassTransit;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Common.Data.IntegrationEvents;

namespace ECommerce.Shared.Common.EventBus.Services;

public sealed class IntegrationEventPublisher<TDbContext>(TDbContext dbContext, IPublishEndpoint publishEndpoint) : IIntegrationEventPublisher
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext = dbContext;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IntegrationEvent
    {
        await _publishEndpoint.Publish(@event, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
