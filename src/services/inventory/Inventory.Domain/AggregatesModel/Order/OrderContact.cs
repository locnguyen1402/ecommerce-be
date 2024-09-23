using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.AggregatesModel.Common;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class OrderContact(
    string contactName,
    string phoneNumber,
    AddressInfo addressInfo,
    string? notes,
    Guid orderId
    ) : AuditedAggregateRoot
{
    public string ContactName { get; private set; } = contactName;
    public string PhoneNumber { get; private set; } = phoneNumber;
    public AddressInfo AddressInfo { get; private set; } = addressInfo;
    public string? Notes { get; private set; } = notes;
    public Guid OrderId { get; private set; } = orderId;
    public virtual Order Order { get; private set; } = null!;

    public void UpdateContactInfo(
        string contactName,
        string phoneNumber,
        AddressInfo addressInfo,
        string notes
    )
    {
        ContactName = contactName;
        PhoneNumber = phoneNumber;
        AddressInfo = addressInfo;
        Notes = notes;
    }

    public void UpdateAddressInfo(AddressInfo addressInfo)
    {
        AddressInfo = addressInfo;
    }

    public string GetFullAddress()
    {
        return AddressInfo.GetFullAddress();
    }
}