using Microsoft.EntityFrameworkCore;

namespace ECommerce.Shared.Common.EventBus.Data;

public class EventBusDbContextFactory(
    IDbContextFactory<EventBusDbContext> pooledFactory
) : IDbContextFactory<EventBusDbContext>
{
    public EventBusDbContext CreateDbContext()
    {
        var context = pooledFactory.CreateDbContext();

        return context;
    }
}
