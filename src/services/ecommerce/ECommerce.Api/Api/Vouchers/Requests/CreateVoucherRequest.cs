using FluentValidation;

using ECommerce.Shared.Common.Enums;

namespace ECommerce.Api.Vouchers.Requests;

public class CreateVoucherRequest
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public VoucherAppliedOnType AppliedOnType { get; set; } = VoucherAppliedOnType.ALL_PRODUCTS;
    public VoucherTargetCustomerType TargetCustomerType { get; set; } = VoucherTargetCustomerType.ALL;
    public VoucherPopularType PopularType { get; set; } = VoucherPopularType.PUBLIC;
    public decimal MinSpend { get; set; } = 0;
    public int MaxQuantity { get; set; } = 0;
    public int MaxQuantityPerUser { get; set; } = 1;
    public VoucherType Type { get; set; } = VoucherType.DISCOUNT;
    public VoucherDiscountType DiscountType { get; set; } = VoucherDiscountType.PERCENTAGE;
    public decimal Value { get; set; } = 0;
    public decimal? MaxValue { get; set; } = 0;
    public HashSet<Guid> ProductIds { get; set; } = [];
}

public class CreateVoucherRequestValidator : AbstractValidator<CreateVoucherRequest>
{
    string PrefixErrorMessage => nameof(CreateVoucherRequestValidator);
    public CreateVoucherRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Code)
            .MaximumLength(100);

        RuleFor(x => x.StartDate)
            .NotEmpty();

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .GreaterThan(x => x.StartDate);

        RuleFor(x => x.ProductIds)
            .Must(x => x.Count == x.Distinct().Count())
            .WithMessage($"{PrefixErrorMessage} Product id must be unique");

        RuleForEach(x => x.ProductIds)
            .Must(id => id != Guid.Empty && Guid.TryParse(id.ToString(), out _))
            .When(x => x.AppliedOnType == VoucherAppliedOnType.SPECIFIC_PRODUCTS)
            .WithMessage($"{PrefixErrorMessage} Invalid product id format");
    }
}