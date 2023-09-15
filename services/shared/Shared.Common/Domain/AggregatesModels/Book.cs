namespace ECommerce.Shared.Common.Domain.AggregatesModels;
public class Book : BaseEntity
{
    public int NumberOfPages { get; set; } = 0;
    public List<string> Contributions { get; set; } = new List<string>();
    public string? PublishCountry { get; set; }
    public string? PublishDate { get; set; }
    public List<string> Series { get; set; } = new List<string>();
    public List<string> Publishers { get; set; } = new List<string>();
    public List<string> Isbn13 { get; set; } = new List<string>();
    public List<string> Isbn10 { get; set; } = new List<string>();
    public BookStatus Status { get; private set; } = BookStatus.OTHER;
    public void UpdateStatus(BookStatus status)
    {
        Status = status;
    }
}

public enum BookStatus
{
    BORROW,
    FULL,
    RESTRICTED,
    NOVIEW,
    OTHER
}