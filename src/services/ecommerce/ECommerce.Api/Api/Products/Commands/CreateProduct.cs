using FluentValidation;

using ECommerce.Shared.Libs.Extensions;
using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Api.Products.Specifications;
using ECommerce.Api.Products.Requests;
using ECommerce.Api.Services;

namespace ECommerce.Api.Products.Commands;

public class CreateProductCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateProductRequest request,
        IValidator<CreateProductRequest> validator,
        IMerchantService merchantService,
        IProductRepository productRepository,
        IProductAttributeRepository productAttributeRepository,
        IShopCollectionRepository shopCollectionRepository,
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

        var merchantId = await merchantService.GetMerchantIdAsync(cancellationToken);

        var slug = string.IsNullOrEmpty(request.Slug) ? request.Name.ToGenerateRandomSlug() : request.Slug;
        var newProduct = new Product(request.Name, slug, request.Sku, request.Description);

        var hasProductVariants = request.Attributes.Count > 0 && request.Variants.Count > 0;

        if (hasProductVariants)
        {
            List<ProductAttribute> selectedAttributes = [];
            var productAttributesSpec = new GetProductAttributesByIdsSpecification([.. request.Attributes]);
            selectedAttributes = (await productAttributeRepository.GetAsync(productAttributesSpec, cancellationToken)).ToList();

            if (selectedAttributes.Count != request.Attributes.Count)
            {
                return Results.BadRequest("Some attributes are not found");
            }

            newProduct.AddOrUpdateAttributes(selectedAttributes);

            if (request.Variants.Count != 0)
            {
                foreach (var variant in request.Variants)
                {
                    var attributeValues = new List<ProductVariantAttributeValue>();

                    foreach (var value in variant.Values)
                    {
                        attributeValues.Add(new(value.ProductAttributeId, value.Value, value.AttributeValueId));
                    }

                    newProduct.AddVariant(variant.Stock, variant.Price, variant.Sku, attributeValues);
                }
            }
        }
        else
        {
            newProduct.AddVariant(request.Stock ?? 0, request.Price ?? 0, string.Empty, []);
        }

        newProduct.SetMerchant(merchantId);

        await productRepository.AddAndSaveChangeAsync(newProduct, cancellationToken);

        return TypedResults.Ok(new IdResponse(newProduct.Id.ToString()));
    };
}