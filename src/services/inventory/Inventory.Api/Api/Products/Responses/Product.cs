using System.Linq.Expressions;
using ECommerce.Inventory.Api.Categories.Responses;
using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Responses;

public record AdminProductDetailResponse(
    Guid Id,
    string Name,
    string Slug,
    string Description,
    List<ProductVariantResponse> Variants,
    List<CategoryResponse> Categories
)
{
    private List<ProductAttributeResponse> ProductAttributes { get; } = [];
    public AdminProductDetailResponse(
        Guid id,
        string name,
        string slug,
        string description,
        List<ProductAttributeResponse> productAttributes,
        List<ProductVariantResponse> variants,
        List<CategoryResponse> categories) : this(id, name, slug, description, variants, categories)
    {
        ProductAttributes = productAttributes;
    }

    public decimal Price => Variants.Min(x => x.Price);
    public List<ProductAttributeWithValuesResponse> Attributes
        => ProductAttributes
            .Select(x => new ProductAttributeWithValuesResponse(
                x.Id, x.Name,
                Variants.SelectMany(v => v.Values.Where(a => a.AttributeId == x.Id).Select(a => a.Value)).ToHashSet())
            )
            .ToList();
};

public static class ProductProjection
{
    public static AdminProductDetailResponse ToAdminProductDetailResponse(this Product product)
    {
        return ToAdminProductDetailResponse().Compile().Invoke(product);
    }

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
                .ToList(),
            x.Categories
                .AsQueryable()
                .Select(CategoryProjection.ToCategoryResponse())
                .ToList()
        );
}