using FluentValidation;

namespace ECommerce.Inventory.Api.Categories.Requests;

public class UpdateCategoryRequest : CreateCategoryRequest
{
    public Guid Id { get; set; }
}

public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    private string PrefixErrorMessage => nameof(UpdateCategoryRequestValidator);
    public UpdateCategoryRequestValidator()
    {
        RuleFor(x => x.Id)
            .Must(id => id != Guid.Empty && Guid.TryParse(id.ToString(), out _))
            .WithMessage($"{PrefixErrorMessage} Invalid Id format");

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}