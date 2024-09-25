using FluentValidation;

namespace ECommerce.Inventory.Api.Products.Requests;

public class ExtendStockRequest
{
    public Guid ProductVariantId { get; set; }
    public int Quantity { get; set; }
}

public class ExtendStockRequestValidator : AbstractValidator<ExtendStockRequest>
{
    private string PrefixErrorMessage => nameof(ExtendStockRequestValidator);
    public ExtendStockRequestValidator()
    {
        RuleFor(x => x.ProductVariantId)
            .Must(id => id != Guid.Empty && Guid.TryParse(id.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid Id format");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(1);
    }
}