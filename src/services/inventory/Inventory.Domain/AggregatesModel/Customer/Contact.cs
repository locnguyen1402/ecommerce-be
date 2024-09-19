using ECommerce.Shared.Common.AggregatesModel.Common;
using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Common.Infrastructure.Data;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Contact(
    AddressType type
    , bool isDefault
) : Entity
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
    public string CustomerId { get; private set; } = null!;
    public virtual Customer Customer { get; private set; } = null!;
}