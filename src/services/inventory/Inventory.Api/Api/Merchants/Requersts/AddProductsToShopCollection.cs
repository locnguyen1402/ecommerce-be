using FluentValidation;

namespace ECommerce.Inventory.Api.Merchants.Requests;

public class AddProductsToShopCollectionRequest
{
    public Guid ShopCollectionId { get; set; }
    public List<Guid> ProductIds { get; set; } = [];
}

public class AddProductsToShopCollectionRequestValidator : AbstractValidator<AddProductsToShopCollectionRequest>
{
    private string PrefixErrorMessage => nameof(AddProductsToShopCollectionRequestValidator);
    public AddProductsToShopCollectionRequestValidator()
    {
        RuleFor(x => x.ShopCollectionId)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid id format");

        RuleForEach(x => x.ProductIds)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid category id format");
    }
}