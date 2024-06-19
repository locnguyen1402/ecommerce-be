using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Products.Specifications;
using ECommerce.Inventory.Api.Products.Requests;
using ECommerce.Inventory.Api.Categories.Specifications;

namespace ECommerce.Inventory.Api.Products.Commands;

public class CreateProductCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateProductRequest request,
        IValidator<CreateProductRequest> validator,
        IProductRepository productRepository,
        IProductAttributeRepository productAttributeRepository,
        ICategoryRepository categoryRepository,
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
        if (request.Attributes.Count > 0)
        {
            var productAttributesSpec = new GetProductAttributesByIdsSpecification([.. request.Attributes]);
            selectedAttributes = (await productAttributeRepository.GetAsync(productAttributesSpec, cancellationToken)).ToList();

            if (selectedAttributes.Count != request.Attributes.Count)
            {
                return Results.BadRequest("Some attributes are not found");
            }

            newProduct.AddOrUpdateAttributes(selectedAttributes);
        }

        List<Category> selectedCategories = [];
        if (request.Categories.Count > 0)
        {
            var categoriesSpec = new GetCategoriesSpecification(x => request.Categories.Contains(x.Id));
            selectedCategories = (await categoryRepository.GetAsync(categoriesSpec, cancellationToken)).ToList();

            if (selectedCategories.Count != request.Categories.Count)
            {
                return Results.BadRequest("Some categories are not found");
            }

            newProduct.AddOrUpdateCategories(selectedCategories);
        }

        if (request.Variants.Count != 0)
        {
            foreach (var variant in request.Variants)
            {
                var attributeValues = new List<ProductVariantAttributeValue>();

                foreach (var value in variant.Values)
                {
                    attributeValues.Add(new(value.ProductAttributeId, value.Value));
                }

                newProduct.AddVariant(variant.Stock, variant.Price, attributeValues);
            }
        }

        await productRepository.AddAndSaveChangeAsync(newProduct, cancellationToken);

        return TypedResults.Ok(new IdResponse(newProduct.Id.ToString()));
    };
}