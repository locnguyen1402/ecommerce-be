namespace ECommerce.Shared.Common.Domain.AggregatesModels;
public class WorkRatings
{
    public float Average { get; set; } = 0;
    public int Count { get; set; } = 0;
    public int Rating1Stars { get; set; } = 0;
    public int Rating2Stars { get; set; } = 0;
    public int Rating3Stars { get; set; } = 0;
    public int Rating4Stars { get; set; } = 0;
    public int Rating5Stars { get; set; } = 0;
}