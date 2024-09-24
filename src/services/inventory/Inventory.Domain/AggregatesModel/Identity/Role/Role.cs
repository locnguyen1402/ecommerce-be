using Microsoft.AspNetCore.Identity;

using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.Constants;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel.Identity;

public class Role : IdentityRole<Guid>, IAggregateRoot, IEntity<Guid>, ICreationAuditing, IUpdateAuditing
{
    public bool Predefined { get; private set; }

    public string? Description { get; private set; } = string.Empty;

    public List<string> Permissions { get; } = [];

    public bool Enabled { get; private set; }

    private readonly List<UserRole> _userRoles;
    public virtual IReadOnlyCollection<UserRole> UserRoles => _userRoles;

    private readonly List<RoleClaim> _roleClaims;
    public virtual IReadOnlyCollection<RoleClaim> RoleClaims => _roleClaims;

    public Role() : base()
    {
        _userRoles = [];
        _roleClaims = [];

        Enabled = true;
    }

    public Role(string roleName) : base(roleName)
    {
        _userRoles = [];
        _roleClaims = [];
    }

    public Role(string roleName, string description) : this(roleName)
        => Describe(description);

    public Role(string roleName, string description, bool predefined) : this(roleName, description)
        => Predefined = predefined;

    public void MakeAsPredefined() => Predefined = true;

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void Describe(string description) => Description = description;

    #region Enable/Disable
    public void Enable()
    {
        Enabled = true;
    }

    public void Disable()
    {
        Enabled = false;
    }
    #endregion Enable/Disable

    public void UpdatePermissions(IEnumerable<string> permissions)
    {
        IEnumerable<string> permissionsToBeAdded = permissions.Except(Permissions);
        IEnumerable<string> permissionsToBeDeleted = Permissions.Except(permissions);

        _roleClaims.AddRange(permissionsToBeAdded.Select(x => new RoleClaim()
        {
            RoleId = Id,
            ClaimType = SecurityClaimTypes.Permission,
            ClaimValue = x
        }));

        _roleClaims.RemoveAll(x => x.ClaimType == SecurityClaimTypes.Permission && permissionsToBeDeleted.Contains(x.ClaimValue!));

        Permissions.Clear();
        Permissions.AddRange(permissions);
    }

    #region Auditing

    #region Auditing Properties

    public Guid? CreatedBy { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    #endregion Auditing Properties

    #region Auditing Methods

    public void AuditCreation(Guid? createdBy)
    {
        CreatedBy = createdBy;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void AuditUpdate(Guid? updatedBy)
    {
        UpdatedBy = updatedBy;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    #endregion Auditing Methods

    #endregion Auditing
}
