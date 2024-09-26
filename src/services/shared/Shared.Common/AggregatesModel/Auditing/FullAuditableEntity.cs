
namespace ECommerce.Shared.Common.AggregatesModel.Auditing;

public class FullAuditableEntity : AuditedEntity, IDeletionAuditing
{
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset? DeletedAt { get; private set; }

    public void AuditDeletion(Guid? deletedBy)
    {
        DeletedBy = deletedBy;
        DeletedAt = DateTimeOffset.UtcNow;
    }
}
