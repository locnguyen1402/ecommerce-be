namespace ECommerce.Shared.Common.AggregatesModel.Auditing;

public interface IUpdateAuditing
{
    Guid? UpdatedBy { get; }
    DateTimeOffset? UpdatedAt { get; }

    void AuditUpdate(Guid? updatedBy);
}
