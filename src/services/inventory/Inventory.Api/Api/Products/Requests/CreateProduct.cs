using FluentValidation;

namespace ECommerce.Inventory.Api.Products.Requests;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public HashSet<Guid> Categories { get; set; } = [];
    public HashSet<Guid> Attributes { get; set; } = [];
    public List<CreateProductVariantRequest> Variants { get; set; } = [];
    public HashSet<CreateProductVariantRequest> HashedVariants => [.. Variants];
}

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    private string PrefixErrorMessage => nameof(CreateProductRequestValidator);
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .NotEmpty();

        RuleFor(x => x.Attributes)
            .Must(x => x.Count == x.Distinct().Count())
            .WithMessage($"{PrefixErrorMessage} Attribute id must be unique");

        RuleForEach(x => x.Attributes)
            .NotEmpty()
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _));

        RuleForEach(x => x.Categories)
            .NotEmpty()
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid category id format");

        RuleFor(x => x.Variants)
            .Must((p, x) => x.Count == p.HashedVariants.Count)
            .WithMessage($"{PrefixErrorMessage} Variant must be unique");

        RuleForEach(x => x.Variants)
            .SetValidator(x => new CreateProductVariantRequestValidator([.. x.Attributes]));
    }
}