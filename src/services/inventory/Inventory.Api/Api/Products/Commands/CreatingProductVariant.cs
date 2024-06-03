using FluentValidation;

namespace ECommerce.Inventory.Api.Products.Commands;

public class CreatingProductVariantAttributeValue
{
    public Guid ProductAttributeId { get; set; }
    public string Value { get; set; } = string.Empty;
}

public class CreatingProductVariantAttributeValueVariantValidator : AbstractValidator<CreatingProductVariantAttributeValue>
{
    public CreatingProductVariantAttributeValueVariantValidator()
    {
        RuleFor(x => x.ProductAttributeId)
            .NotEmpty()
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _));

        RuleFor(x => x.Value)
            .NotEmpty();
    }
}

public class CreatingProductVariant
{
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public List<CreatingProductVariantAttributeValue> Values { get; set; } = [];
    public HashSet<Guid> ProductAttributeIds => Values.Select(x => x.ProductAttributeId).ToHashSet();
}

public class CreateProductVariantValidator : AbstractValidator<CreatingProductVariant>
{
    public CreateProductVariantValidator()
    {
        RuleFor(x => x.Stock)
                    .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Values)
            .Must(x => x.Count == x.Select(x => x.ProductAttributeId).Distinct().Count())
            .WithMessage("Product attribute id must be unique");

        RuleForEach(x => x.Values)
            .SetValidator(new CreatingProductVariantAttributeValueVariantValidator());
    }
    public CreateProductVariantValidator(List<Guid> productAttributeIds) : this()
    {
        RuleFor(x => x.Values)
            .Must(x => x.Count == productAttributeIds.Count)
            .WithMessage("Product attribute id must be equal to the product attribute count that is assigned to product");

        RuleFor(x => x.ProductAttributeIds)
            .Must(ids => ids.All(id => productAttributeIds.Contains(id)))
            .WithMessage("Some product attribute id is not valid");
    }
}