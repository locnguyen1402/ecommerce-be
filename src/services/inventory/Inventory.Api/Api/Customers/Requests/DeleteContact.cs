using FluentValidation;

namespace ECommerce.Inventory.Api.Customers.Requests;

public class DeleteContactRequest
{
    public Guid Id { get; set; }
}

public class DeleteContactRequestValidator : AbstractValidator<DeleteContactRequest>
{
    private string PrefixErrorMessage => nameof(DeleteContactRequestValidator);
    public DeleteContactRequestValidator()
    {
        RuleFor(x => x.Id)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid id format");
    }
}