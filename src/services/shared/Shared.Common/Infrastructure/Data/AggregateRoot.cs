namespace ECommerce.Shared.Common.Infrastructure.Data;

public abstract class AggregateRoot : Entity, IAggregateRoot
{
    protected AggregateRoot() : base() { }

    protected AggregateRoot(Guid id) : base(id) { }
}
