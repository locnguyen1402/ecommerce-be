namespace ECommerce.Shared.Common.Domain.AggregatesModels;
public class Book : BaseEntity
{
    public int NumberOfPages { get; set; } = 0;
    public List<string> Contributions { get; set; } = new List<string>();
    public string? PublishCountry { get; set; }
    public string? PublishDate { get; set; }
}