namespace ECommerce.Products.Api.Application.Responses;

public class ProductCategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

public class ProductCategoryResponseProfile : Profile
{
    public ProductCategoryResponseProfile()
    {
        CreateMap<ProductCategory, ProductCategoryResponse>();
    }
}