namespace ECommerce.Shared.Common.AggregatesModel.Auditing;

public interface ICreationAuditing
{
    Guid? CreatedBy { get; }
    DateTimeOffset CreatedAt { get; }

    void AuditCreation(Guid? createdBy);
}
