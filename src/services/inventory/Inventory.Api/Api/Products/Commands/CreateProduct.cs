using Microsoft.EntityFrameworkCore;

using MediatR;
using FluentValidation;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Inventory.Domain.AggregatesModel;
using System.Data;

namespace ECommerce.Inventory.Api.Products.Commands;

public class CreateProductCommand : IRequest<IdResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public HashSet<Guid> Attributes { get; set; } = [];
    public List<CreatingProductVariant> Variants { get; set; } = [];
}
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Slug)
            .NotEmpty();

        RuleFor(x => x.Attributes)
            .Must(x => x.Count == x.Distinct().Count())
            .WithMessage("Attribute id must be unique");

        RuleForEach(x => x.Attributes)
            .NotEmpty()
            .Must(x => x != Guid.Empty && Guid.TryParse(x.ToString(), out _));

        RuleForEach(x => x.Variants)
            .SetValidator(x => new CreateProductVariantValidator([.. x.Attributes]));
    }
}
public class CreateProductCommandHandler(
    IValidator<CreateProductCommand> validator,
    IProductRepository productRepository,
    IProductAttributeRepository productAttributeRepository
) : IRequestHandler<CreateProductCommand, IdResponse>
{
    public async Task<IdResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
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

        productRepository.Add(newProduct);
        await productRepository.SaveChangesAsync();

        return new IdResponse(newProduct.Id.ToString());
    }
}