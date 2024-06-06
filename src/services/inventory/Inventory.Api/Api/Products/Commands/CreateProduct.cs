using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Products.Specifications;
using ECommerce.Inventory.Api.Products.Requests;
using ECommerce.Inventory.Api.Utilities;

namespace ECommerce.Inventory.Api.Products.Commands;

public class CreateProductCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreatingProductRequest request,
        IValidator<CreatingProductRequest> validator,
        IProductRepository productRepository,
        IProductAttributeRepository productAttributeRepository,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        if (await productRepository.AnyAsync(x => x.Slug == request.Slug, cancellationToken))
        {
            return Results.BadRequest("Slug is already taken");
        }

        var newProduct = new Product(request.Name, request.Slug, request.Description);
        List<ProductAttribute> selectedAttributes = [];

        if (request.Attributes.Count != 0)
        {
            var productAttributesSpec = new GetProductAttributesSpecification(x => request.Attributes.Contains(x.Id));
            selectedAttributes = (await productAttributeRepository.GetAsync(productAttributesSpec, cancellationToken)).ToList();

            if (selectedAttributes.Count != request.Attributes.Count)
            {
                return Results.BadRequest("Some attributes are not found");
            }

            newProduct.AddOrUpdateAttributes(selectedAttributes);
        }

        if (request.Variants.Count != 0)
        {
            foreach (var variant in request.Variants)
            {
                ProductUtils.AddVariantToProduct(newProduct, variant);
            }
        }

        await productRepository.AddAndSaveChangeAsync(newProduct, cancellationToken);

        return TypedResults.Ok(new IdResponse(newProduct.Id.ToString()));
    };
}