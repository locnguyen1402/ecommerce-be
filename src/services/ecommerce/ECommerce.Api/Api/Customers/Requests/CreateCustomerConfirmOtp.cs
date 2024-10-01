using ECommerce.Shared.Common.Constants;
using FluentValidation;

namespace ECommerce.Api.Customers.Requests;

public class CreateCustomerConfirmOtpRequest
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}

public class CreateCustomerConfirmOtpRequestValidator : AbstractValidator<CreateCustomerConfirmOtpRequest>
{
    private string PrefixErrorMessage => nameof(CreateCustomerConfirmOtpRequestValidator);
    public CreateCustomerConfirmOtpRequestValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .Matches(RegexConstants.PhoneNumberPattern)
            .WithMessage("The phone number is invalid");

        RuleFor(x => x.Code)
            .NotNull()
            .NotEmpty()
            .Matches(RegexConstants.CodePattern)
            .WithMessage("The code is invalid");
    }
}