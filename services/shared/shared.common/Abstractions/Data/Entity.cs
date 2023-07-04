using System;

namespace ECommerce.Shared.Common;

public class Entity : IEntity
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}