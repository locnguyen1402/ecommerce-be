using FluentValidation;

using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Products.Requests;
using ECommerce.Api.Products.Responses;

namespace ECommerce.Api.Products.Commands;

public class UpdateProductAttributeCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        UpdateProductAttributeRequest request,
        IValidator<UpdateProductAttributeRequest> validator,
        IProductAttributeRepository productAttributeRepository,
        CancellationToken cancellationToken
    ) =>
    {
        if (id != request.Id)
        {
            return Results.BadRequest();
        }

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        if (await productAttributeRepository.AnyAsync(x => x.Name == request.Name && x.Id != id, cancellationToken))
        {
            return Results.BadRequest("Attribute name is already existed");
        }

        var attribute = await productAttributeRepository.FindAsync(id, cancellationToken);

        if (attribute == null)
        {
            return Results.NotFound();
        }

        attribute.UpdateName(request.Name);

        await productAttributeRepository.UpdateAndSaveChangeAsync(attribute, cancellationToken);

        return TypedResults.Ok(attribute.ToProductAttributeResponse());
    };
}
