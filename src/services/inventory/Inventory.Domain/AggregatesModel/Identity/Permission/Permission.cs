using ECommerce.Shared.Common.AggregatesModel.Auditing;

namespace ECommerce.Inventory.Domain.AggregatesModel.Identity;

public class Permission(string value, string name, string description) : CreationAuditableAggregateRoot
{
    public Guid GroupId { get; init; }

    public string Value { get; init; } = value;

    public string Name { get; private set; } = name;

    public string? Description { get; private set; } = description;

    public virtual PermissionGroup Group { get; private set; } = null!;

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}

