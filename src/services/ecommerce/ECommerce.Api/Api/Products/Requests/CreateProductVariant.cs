using FluentValidation;

using ECommerce.Shared.Common.Helper;

namespace ECommerce.Api.Products.Requests;

public class CreateProductVariantRequest
{
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public string? Sku { get; set; } = string.Empty;
    public HashSet<CreateProductVariantAttributeRequest> Values { get; set; } = [];
    public HashSet<Guid> ProductAttributeIds => Values.Select(x => x.ProductAttributeId).ToHashSet();

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (CreateProductVariantRequest)obj;
        return Values.SetEquals(other.Values);
    }

    public override int GetHashCode()
        => HashCode.Combine(HashCodeHelper.GetListHashCode(Values, true));
}

public class CreateProductVariantRequestValidator : AbstractValidator<CreateProductVariantRequest>
{
    private string PrefixErrorMessage => nameof(CreateProductVariantRequestValidator);
    public CreateProductVariantRequestValidator()
    {
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Sku)
            .MaximumLength(100);

        RuleFor(x => x.Values)
            .Must((b, x) => b.ProductAttributeIds.Count == x.Count)
            .WithMessage($"{PrefixErrorMessage} Attributes in product variant must be unique");

        RuleForEach(x => x.Values)
            .SetValidator(new CreateProductVariantAttributeRequestValidator());
    }
    public CreateProductVariantRequestValidator(List<Guid> productAttributeIds) : this()
    {
        RuleFor(x => x.Values)
            .Must(x => x.Count == productAttributeIds.Count)
            .WithMessage($"{PrefixErrorMessage} Number of attributes in product variant must be equal number of attributes assigned to product");

        RuleFor(x => x.ProductAttributeIds)
            .Must(ids => ids.All(id => productAttributeIds.Contains(id)))
            .WithMessage($"{PrefixErrorMessage} Some attributes in product variant are not assigned to product");
    }
}