namespace ECommerce.Products.Api.Application.Responses;

public class ProductItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Price { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    public ProductCategoryResponse Category { get; set; } = null!;
}

public class ProductItemResponseProfile : Profile
{
    public ProductItemResponseProfile()
    {
        CreateMap<Product, ProductItemResponse>()
            .ForMember(p => p.Category, opt => opt.MapFrom(p => p.ProductCategory))
            .ForMember(p => p.Tags, opt => opt.MapFrom(source => BuildTagList(source.Tags)));
    }

    private static List<string> BuildTagList(string tags)
    {
        var tagList = new List<string>();

        if (!tags.IsNullOrEmpty())
        {
            tagList = tags.Split("/").ToList();
        }

        return tagList;
    }
}