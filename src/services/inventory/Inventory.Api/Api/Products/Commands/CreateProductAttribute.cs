using Microsoft.EntityFrameworkCore;

using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Products.Requests;

namespace ECommerce.Inventory.Api.Products.Commands;

public class CreateProductAttributeCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateProductAttributeRequest request,
        IValidator<CreateProductAttributeRequest> validator,
        IProductAttributeRepository productAttributeRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        if (await productAttributeRepository.AnyAsync(x => x.Name == request.Name, cancellationToken))
        {
            return Results.BadRequest("Attribute name is already taken");
        }

        var newProductAttribute = new ProductAttribute(request.Name);

        await productAttributeRepository.AddAndSaveChangeAsync(newProductAttribute, cancellationToken);

        return TypedResults.Ok(new IdResponse(newProductAttribute.Id.ToString()));
    };
}
