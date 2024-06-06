using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Api.Products.Specifications;
using ECommerce.Inventory.Api.Products.Requests;

namespace ECommerce.Inventory.Api.Products.Commands;

public class CreateProductCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateProductRequest request,
        IValidator<CreateProductRequest> validator,
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

        List<ProductAttribute> selectedAttributes = [];

        if (request.Attributes.Count != 0)
        {
            var productAttributesSpec = new GetProductAttributesSpecification(x => request.Attributes.Contains(x.Id));
            selectedAttributes = (await productAttributeRepository.GetAsync(productAttributesSpec, cancellationToken)).ToList();

            if (selectedAttributes.Count != request.Attributes.Count)
            {
                return Results.BadRequest("Some attributes are not found");
            }
        }

        var newProduct = new Product(request.Name, request.Slug, request.Description);

        if (selectedAttributes.Count != 0)
        {
            newProduct.AddAttributes(selectedAttributes);
        }

        if (request.Variants.Count != 0)
        {
            foreach (var variant in request.Variants)
            {
                AddVariantToProduct(newProduct, variant, selectedAttributes);
            }
        }

        await productRepository.AddAndSaveChangeAsync(newProduct, cancellationToken);

        return TypedResults.Ok(new IdResponse(newProduct.Id.ToString()));
    };

    private static void AddVariantToProduct(Product product, CreatingProductVariantRequest variant, List<ProductAttribute> selectedAttributes)
    {
        var newVariant = new ProductVariant(variant.Stock, variant.Price);

        if (variant.Values.Count != 0)
        {
            foreach (var value in variant.Values)
            {
                var newValue = new ProductVariantAttributeValue(value.Value)
                {
                    ProductAttribute = selectedAttributes.Find(x => x.Id == value.ProductAttributeId)!
                };

                newVariant.AddAttributeValue(newValue);
            }
        }

        product.AddVariant(newVariant);
    }
}