using FluentValidation;

namespace ECommerce.Inventory.Api.Products.Requests;

public class UpdateProductAttributeRequest : CreateProductAttributeRequest
{
    public Guid Id { get; set; }
}

public class UpdateProductAttributeRequestValidator : AbstractValidator<UpdateProductAttributeRequest>
{
    private string PrefixErrorMessage => nameof(UpdateProductAttributeRequestValidator);

    public UpdateProductAttributeRequestValidator()
    {
        RuleFor(x => x.Id)
            .Must(id => id != Guid.Empty && Guid.TryParse(id.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid Id format");

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(20);
    }
}