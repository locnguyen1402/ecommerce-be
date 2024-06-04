using System.Data;
using Microsoft.EntityFrameworkCore;

using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Commands;

public record CreateProductCommand
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public HashSet<Guid> Attributes { get; set; } = [];
    public List<CreatingProductVariant> Variants { get; set; } = [];
    public HashSet<CreatingProductVariant> HashedVariants => [.. Variants];
}
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .NotEmpty();

        RuleFor(x => x.Attributes)
            .Must(x => x.Count == x.Distinct().Count())
            .WithMessage("Attribute id must be unique");

        RuleForEach(x => x.Attributes)
            .NotEmpty()
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _));

        RuleFor(x => x.Variants)
            .Must((p, x) => x.Count == p.HashedVariants.Count)
            .WithMessage("Variant must be unique");

        RuleForEach(x => x.Variants)
            .SetValidator(x => new CreateProductVariantValidator([.. x.Attributes]));
    }
}
public class CreateProductCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        CreateProductCommand request,
        IValidator<CreateProductCommand> validator,
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

        if (await productRepository.Query.AnyAsync(x => x.Slug == request.Slug, cancellationToken))
        {
            throw new Exception("Slug is already existed");
        }

        List<ProductAttribute>? selectedAttributes = [];

        if (request.Attributes.Count != 0)
        {
            selectedAttributes = await productAttributeRepository.Query.Where(x => request.Attributes.Contains(x.Id)).ToListAsync(cancellationToken);

            if (selectedAttributes.Count != request.Attributes.Count)
            {
                throw new Exception("Some attributes are not found");
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

                newProduct.AddVariant(newVariant);
            }
        }

        await productRepository.AddAndSaveChangeAsync(newProduct, cancellationToken);

        return TypedResults.Ok(new IdResponse(newProduct.Id.ToString()));
    };
}