using FluentValidation;

namespace ECommerce.Api.Promotions.Requests;

public class UpdateProductPromotionRequest : CreateProductPromotionRequest
{
    public Guid Id { get; set; }
}

public class UpdateProductPromotionRequestValidator : AbstractValidator<UpdateProductPromotionRequest>
{
    private string PrefixErrorMessage => nameof(UpdateProductPromotionRequestValidator);
    public UpdateProductPromotionRequestValidator()
    {
        Include(new CreateProductPromotionRequestValidator());

        RuleFor(x => x.Id)
            .Must(id => id != Guid.Empty && Guid.TryParse(id.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid Id format");
    }
}