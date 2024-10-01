using ECommerce.Shared.Common.AggregatesModel.Auditing;

namespace ECommerce.Domain.AggregatesModel;

public class Ward(Guid districtId, string name, string code) : FullAuditableAggregateRoot
{
    public Guid DistrictId { get; private set; } = districtId;
    public string Name { get; private set; } = name;
    public string Code { get; private set; } = code;
    public string? ZipCode { get; private set; }
    public virtual District District { get; private set; } = null!;

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void UpdateCode(string code)
    {
        Code = code;
    }

    public void UpdateZipCode(string zipCode)
    {
        ZipCode = zipCode;
    }
}