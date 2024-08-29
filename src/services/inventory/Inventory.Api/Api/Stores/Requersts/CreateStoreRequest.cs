using FluentValidation;

namespace ECommerce.Inventory.Api.Stores.Requests;

public class CreateStoreRequest
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }
    public Guid MerchantId { get; set; }
}

public class CreateStoreRequestValidator : AbstractValidator<CreateStoreRequest>
{
    private string PrefixErrorMessage => nameof(CreateStoreRequestValidator);
    public CreateStoreRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.MerchantId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid merchant id format");
    }
}