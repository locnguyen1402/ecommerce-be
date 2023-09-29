namespace ECommerce.Shared.Integration.Application.Queries;
public class TrendingWorksQuery : PaginationQuery
{
    public TrendingType? Type { get; set; }
}

public enum TrendingType
{
    NOW,
    DAILY,
    WEEKLY,
    MONTHLY,
    YEARLY,
    FOREVER,
}