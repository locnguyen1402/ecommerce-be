namespace ECommerce.Shared.Common.Domain.AggregatesModels;
public class Work
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public string ImgUrlS { get; set; } = string.Empty;
    public string ImgUrlM { get; set; } = string.Empty;
}