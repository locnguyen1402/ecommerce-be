using FluentValidation;

namespace ECommerce.Inventory.Api.Products.Requests;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public HashSet<Guid> Attributes { get; set; } = [];
    public List<CreatingProductVariantRequest> Variants { get; set; } = [];
    public HashSet<CreatingProductVariantRequest> HashedVariants => [.. Variants];
}

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .NotEmpty();

        RuleFor(x => x.Attributes)
            .Must(x => x.Count == x.Distinct().Count())
            .WithMessage($"{nameof(CreateProductRequestValidator)} Attribute id must be unique");

        RuleForEach(x => x.Attributes)
            .NotEmpty()
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _));

        RuleFor(x => x.Variants)
            .Must((p, x) => x.Count == p.HashedVariants.Count)
            .WithMessage($"{nameof(CreateProductRequestValidator)} Variant must be unique");

        RuleForEach(x => x.Variants)
            .SetValidator(x => new CreatingProductVariantRequestValidator([.. x.Attributes]));
    }
}