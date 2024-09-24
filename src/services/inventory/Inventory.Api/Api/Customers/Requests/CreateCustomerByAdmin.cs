using ECommerce.Shared.Common.Enums;
using FluentValidation;

namespace ECommerce.Inventory.Api.Customers.Requests;

public class CreateCustomerByAdminRequest
{
    public string FullName { get; set; } = string.Empty;
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
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(250);
    }
}