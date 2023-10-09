namespace ECommerce.Products.Api.Application.Queries;
public class ProductWorkListQuery : WorkListQuery
{
}

public class ProductWorkListQueryValidator : AbstractValidator<ProductWorkListQuery>
{
    public ProductWorkListQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);
    }
}