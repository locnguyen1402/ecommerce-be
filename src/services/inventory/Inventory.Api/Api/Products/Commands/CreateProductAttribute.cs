using Microsoft.EntityFrameworkCore;

using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

namespace ECommerce.Inventory.Api.Products.Commands;

public record CreateProductAttributeCommand
{
    private string _name = string.Empty;
    public string Name
    {
        get => _name.ToLower();
        set => _name = value;
    }
}

public class CreateProductAttributeCommandValidator : AbstractValidator<CreateProductAttributeCommand>
{
    public CreateProductAttributeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(20);
    }
}

public class CreateProductAttributeCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateProductAttributeCommand command,
        IValidator<CreateProductAttributeCommand> validator,
        IProductAttributeRepository productAttributeRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var existedProductAttribute = await productAttributeRepository.Query.FirstOrDefaultAsync(x => x.Name == command.Name, cancellationToken);

        if (existedProductAttribute != null)
        {
            return TypedResults.Ok(new IdResponse(existedProductAttribute.Id.ToString()));
        }

        var newProductAttribute = new ProductAttribute(command.Name);

        await productAttributeRepository.AddAndSaveChangeAsync(newProductAttribute, cancellationToken);

        return TypedResults.Ok(new IdResponse(newProductAttribute.Id.ToString()));
    };
}
