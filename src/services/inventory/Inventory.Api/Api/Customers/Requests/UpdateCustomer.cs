using ECommerce.Shared.Common.Enums;
using FluentValidation;

namespace ECommerce.Inventory.Api.Customers.Requests;

public class UpdateCustomerRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public Gender Gender { get; set; } = Gender.UNSPECIFIED;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}

public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
{
    private string PrefixErrorMessage => nameof(UpdateCustomerRequestValidator);
    public UpdateCustomerRequestValidator()
    {
        RuleFor(x => x.Id)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid id format");

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