using System.Linq.Expressions;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Infrastructure.Templates.MassUpdates.Products;

namespace ECommerce.Api.Products.Responses;

public record ProductVariantResponse(
    Guid Id,
    int Stock,
    decimal Price,
    List<ProductVariantAttributeValueResponse> Values
);

public static class ProductVariantProjection
{
    public static ImportSalesInfoTemplate ToImportSalesInfoTemplateResponse(this ProductVariant productVariant)
    {
        return ToImportSalesInfoTemplateResponse().Compile().Invoke(productVariant);
    }

    public static Expression<Func<ProductVariant, ProductVariantResponse>> ToProductVariantResponse()
        => x =>
        new ProductVariantResponse(
            x.Id,
            x.Stock,
            x.Price,
            x.ProductVariantAttributeValues
                .AsQueryable()
                .Select(ProductVariantAttributeValueProjection.ToProductVariantAttributeValueResponse())
                .ToList()
        );

    public static Expression<Func<ProductVariant, ImportSalesInfoTemplate>> ToImportSalesInfoTemplateResponse()
        => x =>
        new ImportSalesInfoTemplate(
            x.Product != null ? x.Product.Sku : string.Empty,
            x.Price.ToString(),
            x.Stock.ToString(),
            x.Product != null ? x.Product.Slug : string.Empty,
            x.Product != null ? x.Product.Name : string.Empty,
            x.ProductVariantAttributeValues
                .AsQueryable()
                .Select(y => y)
                .ToList()
        );
}