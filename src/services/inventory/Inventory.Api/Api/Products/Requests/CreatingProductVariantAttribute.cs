using FluentValidation;

namespace ECommerce.Inventory.Api.Products.Requests;

public class CreatingProductVariantAttributeRequest
{
    public Guid ProductAttributeId { get; set; }
    public string Value { get; set; } = string.Empty;

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (CreatingProductVariantAttributeRequest)obj;
        return ProductAttributeId == other.ProductAttributeId && Value == other.Value;
    }

    public override int GetHashCode()
        => HashCode.Combine(ProductAttributeId, Value);
}

public class CreatingProductVariantAttributeRequestValidator : AbstractValidator<CreatingProductVariantAttributeRequest>
{
    public CreatingProductVariantAttributeRequestValidator()
    {
        RuleFor(x => x.ProductAttributeId)
            .NotEmpty()
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _));

        RuleFor(x => x.Value.Trim())
            .NotEmpty();
    }
}