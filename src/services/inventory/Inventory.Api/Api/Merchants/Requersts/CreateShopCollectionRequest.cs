using FluentValidation;

namespace ECommerce.Inventory.Api.Merchants.Requests;

public class CreateShopCollectionRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public List<Guid> Children { get; set; } = [];
}

public class CreateShopCollectionRequestValidator : AbstractValidator<CreateShopCollectionRequest>
{
    private string PrefixErrorMessage => nameof(CreateShopCollectionRequestValidator);
    public CreateShopCollectionRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.ParentId)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .When(x => x.ParentId != null)
            .WithMessage($"{PrefixErrorMessage} Invalid parent id format");
    }
}