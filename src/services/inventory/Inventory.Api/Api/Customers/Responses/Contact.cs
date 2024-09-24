using System.Linq.Expressions;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Api.Customers.Responses;

public record ContactResponse(
    AddressType Type,
    string? Name,
    string? ContactName,
    string? PhoneNumber,
    AddressResponse AddressInfo,
    string? Note,
    bool IsDefault
);

public static class ContactProjection
{
    public static ContactResponse ToContactResponse(this Contact contact)
    {
        return ToContactResponse().Compile().Invoke(contact);
    }

    public static List<ContactResponse>? ToListContactResponse(this IEnumerable<Contact> contacts)
    {
        return contacts.Any() ? contacts.Select(ToContactResponse().Compile()).ToList() : null;
    }

    public static Expression<Func<Contact, ContactResponse>> ToContactResponse()
        => x =>
        new ContactResponse(
            x.Type,
            x.Name,
            x.ContactName,
            x.PhoneNumber,
            x.AddressInfo.ToAddressResponse(),
            x.Notes,
            x.IsDefault
        );
}