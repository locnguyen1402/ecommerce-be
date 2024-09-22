using FluentValidation;

namespace ECommerce.Inventory.Api.Merchants.Requests;

public class CreateMerchantRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<Guid> CategoryIds { get; set; } = [];
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
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleForEach(x => x.CategoryIds)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid category id format");
    }
}