using ECommerce.Shared.Common;

namespace ECommerce.Services.Product;

public class Product : Entity
{
    public string Title { get; set; }
    public string Author { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
    public string Publisher { get; set; } = string.Empty;
    public string? ImageUrlS { get; set; }
    public string? ImageUrlM { get; set; }
    public string? ImageUrlL { get; set; }
    public Guid? ProductSaleInfoId { get; set; }
    public ProductSaleInfo? ProductSaleInfo { get; set; }
    public Product(string title) : base()
    {
        Title = title;
    }

    public void AddSaleInfo(int quantity, decimal price)
    {
        var saleInfo = ProductSaleInfo ?? new ProductSaleInfo { ProductId = Id };

        saleInfo.UpdateQuantity(quantity);
        saleInfo.UpdatePrice(price);

        if(ProductSaleInfo == null) {
            ProductSaleInfo = saleInfo;
        }
    }
}