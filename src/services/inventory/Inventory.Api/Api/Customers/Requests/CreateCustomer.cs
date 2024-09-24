using ECommerce.Shared.Common.Constants;
using FluentValidation;

namespace ECommerce.Inventory.Api.Customers.Requests;

public class CreateCustomerRequest
{
    public string PhoneNumber { get; set; } = string.Empty;
}

public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    private string PrefixErrorMessage => nameof(CreateCustomerRequestValidator);
    public CreateCustomerRequestValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .Matches(RegexConstants.PhoneNumberPattern)
            .WithMessage("The phone number is invalid");
    }
}