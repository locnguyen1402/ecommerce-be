using MassTransit;

using ECommerce.Shared.Common.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace ECommerce.Shared.Common.EventBus.Data;

public class EventBusDbContext(DbContextOptions<EventBusDbContext> options) : BaseDbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}
