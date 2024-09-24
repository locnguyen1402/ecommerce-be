using FluentValidation;

namespace ECommerce.Inventory.Api.Customers.Requests;

public class SetDefaultContactRequest
{
    public Guid Id { get; set; }
}

public class SetDefaultContactRequestValidator : AbstractValidator<SetDefaultContactRequest>
{
    private string PrefixErrorMessage => nameof(SetDefaultContactRequestValidator);
    public SetDefaultContactRequestValidator()
    {
        RuleFor(x => x.Id)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid id format");
    }
}