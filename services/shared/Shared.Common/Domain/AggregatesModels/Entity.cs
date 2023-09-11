namespace ECommerce.Shared.Common.Domain.AggregatesModels;
public class Entity : IEntity
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}