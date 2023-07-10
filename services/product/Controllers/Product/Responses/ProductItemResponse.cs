using AutoMapper;

namespace ECommerce.Services.Product;

public class ProductItemResponse
{
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public int? PublicationYear { get; set; }
    public string? ImageUrlS { get; set; }
    public string? ImageUrlM { get; set; }
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
}

public class ProductItemResponseProfile : Profile
{
    public ProductItemResponseProfile()
    {
        CreateMap<Product, ProductItemResponse>()
            .ForMember(p => p.Quantity, p => p.MapFrom(i => i.ProductSaleInfo == null ? 0 : i.ProductSaleInfo.Quantity))
            .ForMember(p => p.Price, p => p.MapFrom(i => i.ProductSaleInfo == null ? 0 : i.ProductSaleInfo.Price));
    }
}
