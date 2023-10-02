namespace ECommerce.Products.Api.Application.Queries;
public class TrendingProductListQuery : TrendingWorksQuery
{
}

public class TrendingProductListQueryValidator : AbstractValidator<TrendingProductListQuery>
{
    public TrendingProductListQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);
    }
}