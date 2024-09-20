using FluentValidation;

namespace ECommerce.Inventory.Api.Orders.Requests;

public class ProductItemRequest
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ListPrice { get; set; }
    public Guid ProductVariantId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal? VatPercent { get; set; }
}

public class ProductItemRequestValidator : AbstractValidator<ProductItemRequest>
{
    private string PrefixErrorMessage => nameof(ProductItemRequestValidator);
    public ProductItemRequestValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.ListPrice)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _));

        RuleFor(x => x.ProductVariantId)
            .NotEmpty()
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _));
    }
}