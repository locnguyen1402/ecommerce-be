using FluentValidation;

using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Api.Customers.Requests;

public class CreateCustomerByAdminRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public Gender Gender { get; set; } = Gender.UNSPECIFIED;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}

public class CreateCustomerByAdminRequestValidator : AbstractValidator<CreateCustomerByAdminRequest>
{
    private string PrefixErrorMessage => nameof(CreateCustomerByAdminRequestValidator);
    public CreateCustomerByAdminRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.LastName)
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email));
    }
}