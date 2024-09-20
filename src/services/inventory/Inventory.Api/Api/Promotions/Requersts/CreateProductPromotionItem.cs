using FluentValidation;

using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Api.Promotions.Requests;

public class CreateProductPromotionItemRequest
{
    public Guid ProductId { get; set; }
    public List<CreateProductVariantPromotionItemRequest> Variants { get; set; } = [];
    public HashSet<Guid> VariantIds => Variants.Select(x => x.ProductVariantId).ToHashSet();
}

public class CreateProductPromotionItemRequestValidator : AbstractValidator<CreateProductPromotionItemRequest>
{
    private string PrefixErrorMessage => nameof(CreateProductPromotionItemRequestValidator);
    public CreateProductPromotionItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .Must(id => id != Guid.Empty && Guid.TryParse(id.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid Id format");

        RuleFor(x => x.Variants)
            .Must((p, x) => x.Count == p.VariantIds.Count)
            .WithMessage($"{PrefixErrorMessage} Variant must be unique");

        RuleForEach(x => x.Variants)
            .SetValidator(new CreateProductVariantPromotionItemRequestValidator());

    }
}

public class CreateProductVariantPromotionItemRequest
{
    public Guid ProductVariantId { get; set; }
    public bool IsActive { get; set; }
    public decimal DiscountPrice { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public int Quantity { get; private set; }
    public NoProductsPerOrderLimit NoProductsPerOrderLimit { get; private set; } = NoProductsPerOrderLimit.UNLIMITED;
    public int MaxItemsPerOrder { get; private set; } = 0;
}

public class CreateProductVariantPromotionItemRequestValidator : AbstractValidator<CreateProductVariantPromotionItemRequest>
{
    private string PrefixErrorMessage => nameof(CreateProductVariantPromotionItemRequestValidator);
    public CreateProductVariantPromotionItemRequestValidator()
    {
        RuleFor(x => x.ProductVariantId)
            .Must(id => id != Guid.Empty && Guid.TryParse(id.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid Id format");

        RuleFor(x => x.DiscountPrice)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.DiscountPercentage)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.MaxItemsPerOrder)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.NoProductsPerOrderLimit)
            .Must(x =>
                Enum.IsDefined(typeof(NoProductsPerOrderLimit), x)
                && x != NoProductsPerOrderLimit.UNSPECIFIED
            );
    }
}