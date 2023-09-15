namespace ECommerce.Products.Domain.AggregatesModels;
public class Product : Entity
{
    public string Title { get; set; }
    public string Author { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
    public string Publisher { get; set; } = string.Empty;
    public string? ImageUrlS { get; set; }
    public string? ImageUrlM { get; set; }
    public string? ImageUrlL { get; set; }
    public Product(string title) : base()
    {
        Title = title;
    }
}