namespace ECommerce.Products.Api.Application.Responses;

public class ProductTagOptionResponse
{
    public Guid Id { get; set; }
    public string Value { get; set; } = null!;
}

public class ProductTagOptionResponseProfile : Profile
{
    public ProductTagOptionResponseProfile()
    {
        CreateMap<Tag, ProductTagOptionResponse>();
    }
}