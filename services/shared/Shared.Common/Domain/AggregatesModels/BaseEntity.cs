namespace ECommerce.Shared.Common.Domain.AggregatesModels;
public class BaseEntity
{
    public string Id { get; set; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public string? ImageUrlS { get; set; }
    public string? ImageUrlM { get; set; }
    public List<string> CoverImages { get; set; } = new List<string>();
    public RefType RefType { get; set; } = RefType.OTHER;
}

public enum RefType
{
    BOOK,
    WORK,
    OTHER
}