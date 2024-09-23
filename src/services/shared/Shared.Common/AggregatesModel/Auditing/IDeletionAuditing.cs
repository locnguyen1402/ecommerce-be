namespace ECommerce.Shared.Common.AggregatesModel.Auditing;

public interface IDeletionAuditing
{
    Guid? DeletedBy { get; }
    DateTimeOffset? DeletedAt { get; }

    void AuditDeletion(Guid? deletedBy);
}
