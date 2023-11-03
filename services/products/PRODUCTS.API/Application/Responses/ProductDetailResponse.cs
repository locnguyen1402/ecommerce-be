namespace ECommerce.Products.Api.Application.Responses;

public class ProductDetailResponse : ProductItemResponse
{
}

public class ProductDetailResponseProfile : Profile
{
    public ProductDetailResponseProfile()
    {
        CreateMap<Product, ProductDetailResponse>()
            .IncludeBase<Product, ProductItemResponse>();
    }

}