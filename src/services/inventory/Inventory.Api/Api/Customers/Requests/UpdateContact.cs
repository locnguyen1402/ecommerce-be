using ECommerce.Shared.Common.Enums;
using FluentValidation;

namespace ECommerce.Inventory.Api.Customers.Requests;

public class UpdateContactRequest
{
    public Guid Id { get; set; }
    public AddressType AddressType { get; set; } = AddressType.HOME;
    public string? Name { get; set; }
    public string? ContactName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Notes { get; set; }
    public bool IsDefault { get; set; } = true;
    public AddressRequest AddressInfo { get; set; } = null!;
}

public class UpdateContactRequestValidator : AbstractValidator<UpdateContactRequest>
{
    private string PrefixErrorMessage => nameof(UpdateContactRequestValidator);
    public UpdateContactRequestValidator()
    {
        RuleFor(x => x.Id)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid id format");

        RuleFor(x => x.AddressInfo)
            .SetValidator(x => new AddressRequestValidator());
    }
}