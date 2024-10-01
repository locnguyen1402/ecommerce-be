using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.AggregatesModel.Common;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Domain.AggregatesModel;

public class Contact : AuditedAggregateRoot
{
    public AddressType Type { get; private set; } = AddressType.UNSPECIFIED;

    // Name of contact
    public string? Name { get; private set; }

    // Name of customer/receiver
    public string? ContactName { get; private set; }
    public string? PhoneNumber { get; private set; }
    public AddressInfo AddressInfo { get; private set; } = null!;
    public string? Notes { get; private set; }
    public bool IsDefault { get; private set; } = true;
    public Guid CustomerId { get; private set; }
    public virtual Customer Customer { get; private set; } = null!;

    public void UpdateInfo(
        AddressType type
        , bool isDefault
        , string? name
        , string? contactName
        , string? phoneNumber
        , string? notes
    )
    {
        Type = type;
        Name = name;
        ContactName = contactName;
        PhoneNumber = phoneNumber;
        Notes = notes;
        IsDefault = isDefault;
    }

    public void AssignAddress(AddressInfo address)
    {
        AddressInfo = address;
    }

    public void SetContactDefault()
    {
        IsDefault = true;
    }

    public void RemoveContactDefault()
    {
        IsDefault = false;
    }

    public void AssignToCustomer(Guid customerId)
    {
        CustomerId = customerId;
    }
}