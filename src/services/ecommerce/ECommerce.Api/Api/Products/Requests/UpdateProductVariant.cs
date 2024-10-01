using FluentValidation;

namespace ECommerce.Api.Products.Requests;

public class UpdateProductVariantRequest : CreateProductVariantRequest
{
    public Guid? Id { get; set; }
}

public class UpdateProductVariantRequestValidator : AbstractValidator<UpdateProductVariantRequest>
{
    private string PrefixErrorMessage => nameof(UpdateProductVariantRequestValidator);
    public UpdateProductVariantRequestValidator()
    {
        RuleFor(x => x.Id)
            .Must(id => id == null || Guid.TryParse(id.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid Id format");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Values)
            .Must((b, x) => b.ProductAttributeIds.Count == x.Count)
            .WithMessage($"{PrefixErrorMessage} Attributes in product variant must be unique");

        RuleForEach(x => x.Values)
            .SetValidator(new CreateProductVariantAttributeRequestValidator());
    }
    public UpdateProductVariantRequestValidator(List<Guid> productAttributeIds) : this()
    {
        RuleFor(x => x.Values)
            .Must(x => x.Count == productAttributeIds.Count)
            .WithMessage($"{PrefixErrorMessage} Number of attributes in product variant must be equal number of attributes assigned to product");

        RuleFor(x => x.ProductAttributeIds)
            .Must(ids => ids.All(id => productAttributeIds.Contains(id)))
            .WithMessage($"{PrefixErrorMessage} Some attributes in product variant are not assigned to product");
    }
}