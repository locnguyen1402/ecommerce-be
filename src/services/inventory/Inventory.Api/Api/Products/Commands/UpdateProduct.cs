using FluentValidation;
using Microsoft.EntityFrameworkCore;

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
        ICategoryRepository categoryRepository,
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

        List<ProductAttribute> selectedAttributes = [];
        if (request.Attributes.Count != 0)
        {
            var productAttributesSpec = new GetProductAttributesByIdsSpecification([.. request.Attributes]);
            selectedAttributes = (await productAttributeRepository.GetAsync(productAttributesSpec, cancellationToken)).ToList();

            if (selectedAttributes.Count != request.Attributes.Count)
            {
                return Results.BadRequest("Some attributes are not found");
            }
        }

        var product = await productRepository.Query
                        .Include(p => p.ShopCollections)
                        .Include(p => p.ProductAttributes)
                        .Include(p => p.ProductVariants)
                            .ThenInclude(pv => pv.ProductVariantAttributeValues)
                        .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (product == null)
        {
            return Results.NotFound();
        }

        product.UpdateGeneralInfo(
            request.Name
            , string.IsNullOrEmpty(request.Slug) ? product.Slug : request.Slug
            , request.Description);

        var inPassHasVariants = product.ProductAttributes.Count > 0 && product.ProductVariants.Count > 0;
        var inRequestHasVariants = request.Attributes.Count > 0 && request.Variants.Count > 0;

        if (inRequestHasVariants)
        {
            if (!inPassHasVariants)
            {
                product.RemoveVariant(product.ProductVariants.First());
            }

            product.AddOrUpdateAttributes(selectedAttributes);

            ProductUtils.UpdateVariantsInProduct(product, request.Variants);
        }
        else
        {
            if (inPassHasVariants)
            {
                product.RemoveVariants([.. product.ProductVariants]);
                product.AddVariant(request.Stock ?? 0, request.Price ?? 0, []);
            }
            else
            {
                var defaultVariant = product.ProductVariants.First();

                defaultVariant.UpdateStock(request.Stock ?? defaultVariant.Stock);
                defaultVariant.UpdatePrice(request.Price ?? defaultVariant.Price);
            }

        }

        await productRepository.UpdateAndSaveChangeAsync(product, cancellationToken);

        return TypedResults.Ok(product.ToAdminProductDetailResponse());
    };
}