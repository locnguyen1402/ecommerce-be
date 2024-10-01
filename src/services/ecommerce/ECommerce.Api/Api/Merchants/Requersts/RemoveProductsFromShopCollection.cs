using FluentValidation;

namespace ECommerce.Api.Merchants.Requests;

public class RemoveProductsFromShopCollectionRequest
{
    public Guid ShopCollectionId { get; set; }
    public List<Guid> ProductIds { get; set; } = [];
}

public class RemoveProductsFromShopCollectionRequestValidator : AbstractValidator<RemoveProductsFromShopCollectionRequest>
{
    private string PrefixErrorMessage => nameof(RemoveProductsFromShopCollectionRequestValidator);
    public RemoveProductsFromShopCollectionRequestValidator()
    {
        RuleFor(x => x.ShopCollectionId)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid id format");

        RuleForEach(x => x.ProductIds)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid product id format");
    }
}