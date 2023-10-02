namespace ECommerce.Shared.Common.Domain.AggregatesModels;
public class SearchResultItem
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public RefType RefType { get; set; } = RefType.OTHER;
    public string? LendingBookId { get; set; }
    public string CoverBookId { get; set; } = string.Empty;
    // public int? CoverImageId { get; set; }
    public string? CoverImageUrl { get; set; }
    public bool HasFullText { get; set; } = false;
    public BookStatus? LendingBookStatus { get; set; }
    public int WantToReadCount { get; set; } = 0;
    public int AlreadyReadCount { get; set; } = 0;
    public List<string> RelatedBookImgs { get; set; } = new List<string>();
    public int? FirstPublishYear { get; set; }
    public List<IdName> Authors { get; set; } = new List<IdName>();
    public float RatingsAverage { get; set; } = 0;
    public int RatingsCount { get; set; } = 0;
    public int EditionCount { get; set; } = 0;
    public List<string> EditionKeys { get; set; } = new List<string>();
    public string? FirstEditionId { get; set; }
}