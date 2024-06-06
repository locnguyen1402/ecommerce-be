using FluentValidation;

using ECommerce.Shared.Common.Helper;

namespace ECommerce.Inventory.Api.Products.Requests;

public class CreatingProductVariantRequest
{
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public HashSet<CreatingProductVariantAttributeRequest> Values { get; set; } = [];
    public HashSet<Guid> ProductAttributeIds => Values.Select(x => x.ProductAttributeId).ToHashSet();

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (CreatingProductVariantRequest)obj;
        return Values.SetEquals(other.Values);
    }

    public override int GetHashCode()
        => HashCode.Combine(HashCodeHelper.GetListHashCode(Values, true));
}

public class CreatingProductVariantRequestValidator : AbstractValidator<CreatingProductVariantRequest>
{
    public CreatingProductVariantRequestValidator()
    {
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Values)
            .Must((b, x) => b.ProductAttributeIds.Count == x.Count)
            .WithMessage($"{nameof(CreatingProductVariantRequestValidator)} Attributes in product variant must be unique");

        RuleForEach(x => x.Values)
            .SetValidator(new CreatingProductVariantAttributeRequestValidator());
    }
    public CreatingProductVariantRequestValidator(List<Guid> productAttributeIds) : this()
    {
        RuleFor(x => x.Values)
            .Must(x => x.Count == productAttributeIds.Count)
            .WithMessage($"{nameof(CreatingProductVariantRequestValidator)} Number of attributes in product variant must be equal number of attributes assigned to product");

        RuleFor(x => x.ProductAttributeIds)
            .Must(ids => ids.All(id => productAttributeIds.Contains(id)))
            .WithMessage($"{nameof(CreatingProductVariantRequestValidator)} Some attributes in product variant are not assigned to product");
    }
}