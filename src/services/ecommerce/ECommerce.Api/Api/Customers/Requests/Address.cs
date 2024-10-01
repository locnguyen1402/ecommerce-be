using FluentValidation;

namespace ECommerce.Api.Customers.Requests;

public class AddressRequest
{
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
    public string? AddressLine2 { get; set; }
}

public class AddressRequestValidator : AbstractValidator<AddressRequest>
{
    private string PrefixErrorMessage => nameof(AddressRequestValidator);
    public AddressRequestValidator()
    {
        RuleFor(x => x.ProvinceId)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid provinceId format");

        RuleFor(x => x.ProvinceName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.DistrictId)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid districtId format");

        RuleFor(x => x.DistrictName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.WardId)
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid wardId format");

        RuleFor(x => x.WardName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.AddressLine1)
            .NotEmpty()
            .MaximumLength(250);
    }
}