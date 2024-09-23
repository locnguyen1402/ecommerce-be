using ECommerce.Shared.Common.AggregatesModel.Auditing;

namespace ECommerce.Inventory.Domain.AggregatesModel.Identity;

public class PermissionGroup(string name, string description) : AuditedAggregateRoot
{
    public string Name { get; private set; } = name;

    public string? Description { get; private set; } = description;

    private readonly List<Permission> _permissions = [];

    public virtual IReadOnlyCollection<Permission> Permissions => _permissions;

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void AddPermission(Permission permission)
    {
        _permissions.Add(permission);
    }

    public void RemovePermission(Permission permission)
    {
        _permissions.Remove(permission);
    }

    public void ClearPermissions()
    {
        _permissions.Clear();
    }

    public bool HasPermission(string permissionName)
    {
        return _permissions.Any(p => p.Name == permissionName);
    }
}

