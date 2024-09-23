using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.AggregatesModel.Common;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Contact(
    AddressType type
    , bool isDefault
) : AuditedAggregateRoot
{
    public AddressType Type { get; private set; } = type;

    // Name of contact
    public string? Name { get; private set; }

    // Name of customer/receiver
    public string? ContactName { get; private set; }
    public string? PhoneNumber { get; private set; }
    public AddressInfo AddressInfo { get; private set; } = null!;
    public string? Notes { get; private set; }
    public bool IsDefault { get; private set; } = isDefault;
    public Guid CustomerId { get; private set; }
    public virtual Customer Customer { get; private set; } = null!;
}