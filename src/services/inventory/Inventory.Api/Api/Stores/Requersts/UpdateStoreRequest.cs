using FluentValidation;

namespace ECommerce.Inventory.Api.Stores.Requests;

public class UpdateStoreRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }
}

public class UpdateStoreRequestValidator : AbstractValidator<UpdateStoreRequest>
{
    private string PrefixErrorMessage => nameof(UpdateStoreRequestValidator);
    public UpdateStoreRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}