using FluentValidation;

namespace ECommerce.Inventory.Api.Merchants.Requests;

public class UpdateShopCollectionRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public List<Guid> Children { get; set; } = [];
}

public class UpdateShopCollectionRequestValidator : AbstractValidator<UpdateShopCollectionRequest>
{
    private string PrefixErrorMessage => nameof(UpdateShopCollectionRequestValidator);
    public UpdateShopCollectionRequestValidator()
    {
        RuleFor(x => x.Id)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid id format");

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.Children)
            .Must((request, x) => !x.Contains(request.Id))
            .WithMessage($"{PrefixErrorMessage} Invalid children id format");

        RuleFor(x => x.ParentId)
            .Must((request, x) => x != Guid.Empty && Guid.TryParse(x.ToString(), out _) && request.Id != x)
            .When(x => x.ParentId != null)
            .WithMessage($"{PrefixErrorMessage} Invalid parent id format");
    }
}