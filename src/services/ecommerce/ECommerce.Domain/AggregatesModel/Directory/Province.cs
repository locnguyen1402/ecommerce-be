using ECommerce.Shared.Common.AggregatesModel.Auditing;

namespace ECommerce.Domain.AggregatesModel;

public class Province(string name, string code) : FullAuditableAggregateRoot
{
    public string Name { get; private set; } = name;
    public string Code { get; private set; } = code;
    private readonly List<District> _districts = [];
    public virtual IReadOnlyCollection<District> Districts => _districts;

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void UpdateCode(string code)
    {
        Code = code;
    }
}