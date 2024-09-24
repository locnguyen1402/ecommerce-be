using System.Linq.Expressions;

using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Responses;

public record FilteredProductResponse(
    Guid Id,
    string Name
)
{
    private List<ProductVariant> ProductVariants { get; } = [];
    public int Stock => ProductVariants.Sum(x => x.Stock);
    public decimal Price => ProductVariants.Count > 0 ? ProductVariants.Min(x => x.Price) : 0;
    public FilteredProductResponse(Guid id, string name, List<ProductVariant> productVariants)
        : this(id, name)
    {
        ProductVariants = productVariants;
    }
}

public record AdminProductDetailResponse(
    Guid Id,
    string Name,
    string Slug,
    string Description
)
{
    private List<ProductAttributeResponse> ProductAttributes { get; } = [];
    private List<ProductVariantResponse> ProductVariants { get; } = [];
    public AdminProductDetailResponse(
        Guid id,
        string name,
        string slug,
        string description,
        List<ProductAttributeResponse> productAttributes,
        List<ProductVariantResponse> productVariants
        ) : this(id, name, slug, description)
    {
        ProductAttributes = productAttributes;
        ProductVariants = productVariants;
    }

    public decimal Price => ProductVariants.Count > 0 ? ProductVariants.Min(x => x.Price) : 0;
    public List<ProductAttributeWithValuesResponse> Attributes
        => ProductAttributes
            .Select(x => new ProductAttributeWithValuesResponse(
                x.Id
                , x.Name
                , ProductVariants
                    .SelectMany(v => v.Values.Where(a => a.AttributeId == x.Id).Select(a => a.Value))
                    .Order()
                    .ToHashSet()
            ))
            .OrderBy(x => x.Name)
            .ToList();

    public List<ProductVariantResponse> Variants
        => ProductVariants
            .Select(variant =>
            {
                return new
                {
                    Variant = variant,
                    SortKey = string.Join("|", variant.Values.OrderBy(x => ProductAttributes.Find(xx => xx.Id == x.AttributeId)!.Name).Select(x => x.Value))
                };
            })
            .OrderBy(x => x.SortKey)
            .Select(x => x.Variant)
            .ToList();
};

public static class ProductProjection
{
    public static AdminProductDetailResponse ToAdminProductDetailResponse(this Product product)
    {
        return ToAdminProductDetailResponse().Compile().Invoke(product);
    }

    public static Expression<Func<Product, FilteredProductResponse>> ToFilteredProductResponse()
        => x =>
        new FilteredProductResponse(
            x.Id,
            x.Name,
            x.ProductVariants
                .AsQueryable()
                .ToList()
        );

    public static Expression<Func<Product, AdminProductDetailResponse>> ToAdminProductDetailResponse()
        => x =>
        new AdminProductDetailResponse(
            x.Id,
            x.Name,
            x.Slug,
            x.Description,
            x.ProductAttributes
                .AsQueryable()
                .Select(ProductAttributeProjection.ToProductAttributeResponse())
                .ToList(),
            x.ProductVariants
                .AsQueryable()
                .Select(ProductVariantProjection.ToProductVariantResponse())
                .ToList()
        );
}