using ECommerce.Shared.Common.AggregatesModel.Auditing;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class District(Guid provinceId, string name, string code) : FullAuditableAggregateRoot
{
    public Guid ProvinceId { get; private set; } = provinceId;
    public string Name { get; private set; } = name;
    public string Code { get; private set; } = code;
    public virtual Province Province { get; private set; } = null!;

    private readonly List<Ward> _wards = [];
    public virtual IReadOnlyCollection<Ward> Wards => _wards;

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void UpdateCode(string code)
    {
        Code = code;
    }
}