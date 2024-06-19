using Microsoft.EntityFrameworkCore;
using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Products.Specifications;
using ECommerce.Inventory.Api.Products.Requests;
using ECommerce.Inventory.Api.Utilities;
using ECommerce.Inventory.Api.Products.Responses;

namespace ECommerce.Inventory.Api.Products.Commands;

public class UpdateProductCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        Guid id,
        UpdateProductRequest request,
        IValidator<UpdateProductRequest> validator,
        IProductRepository productRepository,
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

        if (await productRepository.AnyAsync(x => x.Slug == request.Slug && x.Id != id, cancellationToken))
        {
            return Results.BadRequest("Slug is already existed");
        }

        // var oldVariantIds = request.Variants.Where(x => x.Id != null).Select(x => x.Id).ToList();

        var product = await productRepository.Query
                        .Include(p => p.ProductAttributes)
                        .Include(p => p.ProductVariants)
                            .ThenInclude(pv => pv.ProductVariantAttributeValues)
                        .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (product == null)
        {
            return Results.NotFound();
        }

        product.UpdateGeneralInfo(request.Name, request.Slug, request.Description);

        List<ProductAttribute> selectedAttributes = [];

        if (request.Attributes.Count != 0)
        {
            var productAttributesSpec = new GetProductAttributesByIdsSpecification([.. request.Attributes]);
            selectedAttributes = (await productAttributeRepository.GetAsync(productAttributesSpec, cancellationToken)).ToList();

            if (selectedAttributes.Count != request.Attributes.Count)
            {
                return Results.BadRequest("Some attributes are not found");
            }

            product.AddOrUpdateAttributes(selectedAttributes);
        }

        ProductUtils.UpdateVariantsInProduct(product, request.Variants);

        await productRepository.UpdateAndSaveChangeAsync(product, cancellationToken);

        return TypedResults.Ok(product.ToProductResponse());
    };
}