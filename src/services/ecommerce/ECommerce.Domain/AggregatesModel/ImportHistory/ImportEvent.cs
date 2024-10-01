using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Libs.Domain;

namespace ECommerce.Domain.AggregatesModel;

public class ImportEvent : ValueObject
{
    public ImportStatus Status { get; init; }
    public Guid CreatedBy { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public string Remarks { get; init; }

    public ImportEvent(ImportStatus status, Guid createdBy, DateTimeOffset createdAt, string remarks)
    {
        Status = status;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        Remarks = remarks;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Status;
        yield return CreatedBy;
        yield return CreatedAt;
        yield return Remarks;
    }
}
