using FluentValidation;

namespace ECommerce.Inventory.Api.Merchants.Requests;

public class UpdateMerchantRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class UpdateMerchantRequestValidator : AbstractValidator<UpdateMerchantRequest>
{
    private string PrefixErrorMessage => nameof(UpdateMerchantRequestValidator);
    public UpdateMerchantRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}