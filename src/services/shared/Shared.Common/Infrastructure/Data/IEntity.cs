namespace ECommerce.Shared.Common.Infrastructure.Data;

public interface IEntity { }

public interface IEntity<TKey> : IEntity
    where TKey : IEquatable<TKey>
{
    TKey Id { get; }
}