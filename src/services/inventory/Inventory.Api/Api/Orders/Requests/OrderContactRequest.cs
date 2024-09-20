using FluentValidation;

namespace ECommerce.Inventory.Api.Orders.Requests;

public class OrderContactRequest
{
    public string ContactName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Guid ProvinceId { get; set; }
    public string ProvinceName { get; set; } = string.Empty;
    public string ProvinceCode { get; set; } = string.Empty;
    public Guid DistrictId { get; set; }
    public string DistrictName { get; set; } = string.Empty;
    public string DistrictCode { get; set; } = string.Empty;
    public Guid WardId { get; set; }
    public string WardName { get; set; } = string.Empty;
    public string WardCode { get; set; } = string.Empty;
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public string? AddressLine3 { get; set; }
    public string Notes { get; set; } = string.Empty;
}

public class OrderContactRequestValidator : AbstractValidator<OrderContactRequest>
{
    private string PrefixErrorMessage => nameof(OrderContactRequestValidator);
    public OrderContactRequestValidator()
    {
        RuleFor(x => x.ContactName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.ProvinceId)
            .NotEmpty()
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _));

        RuleFor(x => x.DistrictId)
            .NotEmpty()
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _));

        RuleFor(x => x.WardId)
            .NotEmpty()
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _));
    }
}