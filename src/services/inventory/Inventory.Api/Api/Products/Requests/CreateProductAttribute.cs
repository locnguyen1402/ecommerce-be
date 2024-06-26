using FluentValidation;

namespace ECommerce.Inventory.Api.Products.Requests;

public class CreateProductAttributeRequest
{
    private string _name = string.Empty;
    public string Name
    {
        get => _name.ToLower();
        set => _name = value;
    }
}

public class CreateProductAttributeRequestValidator : AbstractValidator<CreateProductAttributeRequest>
{
    public CreateProductAttributeRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(20);
    }
}