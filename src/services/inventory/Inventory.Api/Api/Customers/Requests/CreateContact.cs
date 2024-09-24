using ECommerce.Shared.Common.Enums;
using FluentValidation;

namespace ECommerce.Inventory.Api.Customers.Requests;

public class CreateContactRequest
{
    public AddressType AddressType { get; set; } = AddressType.HOME;
    public string? Name { get; set; }
    public string? ContactName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Notes { get; set; }
    public bool IsDefault { get; set; } = true;
    public AddressRequest AddressInfo { get; set; } = null!;
}

public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
{
    private string PrefixErrorMessage => nameof(CreateContactRequestValidator);
    public CreateContactRequestValidator()
    {
        RuleFor(x => x.AddressInfo)
            .SetValidator(x => new AddressRequestValidator());
    }
}