using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Shared.Common.AggregatesModel.Auditing;

public class CreationAuditableEntity : Entity, ICreationAuditing
{
    public Guid? CreatedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    Guid? ICreationAuditing.CreatedBy => throw new NotImplementedException();

    public void AuditCreation(Guid? createdBy)
    {
        CreatedBy = createdBy;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}
