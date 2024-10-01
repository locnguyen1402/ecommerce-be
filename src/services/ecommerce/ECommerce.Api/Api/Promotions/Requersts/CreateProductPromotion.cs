using System.Data;
using FluentValidation;

using ECommerce.Shared.Common.Enums;

namespace ECommerce.Api.Promotions.Requests;

public class CreateProductPromotionRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; } = string.Empty;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public List<CreateProductPromotionItemRequest> Items { get; set; } = [];
    public HashSet<Guid> ProductIds => Items.Select(x => x.ProductId).ToHashSet();
}

public class CreateProductPromotionRequestValidator : AbstractValidator<CreateProductPromotionRequest>
{
    private string PrefixErrorMessage => nameof(CreateProductPromotionRequestValidator);
    public CreateProductPromotionRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .MaximumLength(200);

        RuleFor(x => x.StartDate)
            .NotEmpty();

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .GreaterThan(x => x.StartDate);

        RuleFor(x => x.Items)
            .Must((p, x) => x.Count == p.ProductIds.Count)
            .WithMessage($"{PrefixErrorMessage} Product must be unique");

        RuleForEach(x => x.Items)
            .SetValidator(new CreateProductPromotionItemRequestValidator());
    }
}