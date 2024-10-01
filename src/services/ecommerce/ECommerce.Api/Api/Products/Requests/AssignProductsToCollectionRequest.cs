using FluentValidation;

namespace ECommerce.Api.Products.Requests;

public class AssignProductsToCollectionRequest
{
    public Guid ShopCollectionId { get; set; }
    public List<Guid> ProductIds { get; set; } = [];
}

public class AssignProductsToCollectionRequestRequestValidator : AbstractValidator<AssignProductsToCollectionRequest>
{
    private string PrefixErrorMessage => nameof(AssignProductsToCollectionRequestRequestValidator);
    public AssignProductsToCollectionRequestRequestValidator()
    {
        RuleFor(x => x.ShopCollectionId)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid shop collection id format");

        RuleForEach(x => x.ProductIds)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid product id format");
    }
}