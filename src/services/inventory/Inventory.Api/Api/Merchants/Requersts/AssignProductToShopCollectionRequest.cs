using FluentValidation;

namespace ECommerce.Inventory.Api.Merchants.Requests;

public class AssignProductToShopCollectionRequest
{
    public Guid MerchantId { get; set; }
    public Guid ShopCollectionId { get; set; }
    public List<Guid> ProductIds { get; set; } = [];
}

public class AssignProductToShopCollectionRequestValidator : AbstractValidator<AssignProductToShopCollectionRequest>
{
    private string PrefixErrorMessage => nameof(AssignProductToShopCollectionRequestValidator);
    public AssignProductToShopCollectionRequestValidator()
    {
        RuleFor(x => x.MerchantId)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid merchant id format");

        RuleFor(x => x.ShopCollectionId)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid shop collection id format");

        RuleForEach(x => x.ProductIds)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid product id format");
    }
}