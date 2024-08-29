using ECommerce.Shared.Common.Enums;
using FluentValidation;

namespace ECommerce.Inventory.Api.Discounts.Requests;

public class UpdateDiscountRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DiscountType Type { get; set; }
    public string? Description { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public decimal? DiscountAmount { get; set; }
    public decimal? MaxDiscountAmount { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public int? LimitationTimes { get; set; }
    public DiscountLimitationType? LimitationType { get; set; }
    public HashSet<Guid> AppliedEntityIds { get; set; } = [];
}

public class UpdateDiscountRequestValidator : AbstractValidator<UpdateDiscountRequest>
{
    private string PrefixErrorMessage => nameof(UpdateDiscountRequestValidator);
    public UpdateDiscountRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.AppliedEntityIds)
            .Must(x => x.Count == x.Distinct().Count())
            .When(x => x.Type == DiscountType.ASSIGNED_TO_CATEGORY || x.Type == DiscountType.ASSIGNED_TO_PRODUCT)
            .WithMessage($"{PrefixErrorMessage} Category id or Product id must be unique");

        RuleForEach(x => x.AppliedEntityIds)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .When(x => x.Type == DiscountType.ASSIGNED_TO_CATEGORY || x.Type == DiscountType.ASSIGNED_TO_PRODUCT && x.AppliedEntityIds.Count > 0)
            .WithMessage($"{PrefixErrorMessage} Invalid category id or product id format");
    }
}