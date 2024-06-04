using ECommerce.Shared.Common.Helper;
using FluentValidation;

namespace ECommerce.Inventory.Api.Products.Commands;

public class CreatingProductVariantAttributeValue
{
    public Guid ProductAttributeId { get; set; }
    public string Value { get; set; } = string.Empty;

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (CreatingProductVariantAttributeValue)obj;
        return ProductAttributeId == other.ProductAttributeId && Value == other.Value;
    }

    public override int GetHashCode()
        => HashCode.Combine(ProductAttributeId, Value);
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
    public HashSet<CreatingProductVariantAttributeValue> Values { get; set; } = [];
    public HashSet<Guid> ProductAttributeIds => Values.Select(x => x.ProductAttributeId).ToHashSet();
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (CreatingProductVariant)obj;
        return Values.SetEquals(other.Values);
    }

    public override int GetHashCode()
        => HashCode.Combine(HashCodeHelper.GetListHashCode(Values, true));
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
            .Must((b, x) => b.ProductAttributeIds.Count == x.Count)
            .WithMessage($"{nameof(CreateProductVariantValidator)} Attributes in product variant must be unique");

        RuleForEach(x => x.Values)
            .SetValidator(new CreatingProductVariantAttributeValueVariantValidator());
    }
    public CreateProductVariantValidator(List<Guid> productAttributeIds) : this()
    {
        RuleFor(x => x.Values)
            .Must(x => x.Count == productAttributeIds.Count)
            .WithMessage($"{nameof(CreateProductVariantValidator)} Number of attributes in product variant must be equal number of attributes assigned to product");

        RuleFor(x => x.ProductAttributeIds)
            .Must(ids => ids.All(id => productAttributeIds.Contains(id)))
            .WithMessage($"{nameof(CreateProductVariantValidator)} Some attributes in product variant are not assigned to product");
    }
}