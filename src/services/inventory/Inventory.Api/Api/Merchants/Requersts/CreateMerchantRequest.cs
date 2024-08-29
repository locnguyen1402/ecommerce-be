using FluentValidation;

namespace ECommerce.Inventory.Api.Merchants.Requests;

public class CreateMerchantRequest
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CreateMerchantRequestValidator : AbstractValidator<CreateMerchantRequest>
{
    private string PrefixErrorMessage => nameof(CreateMerchantRequestValidator);
    public CreateMerchantRequestValidator()
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