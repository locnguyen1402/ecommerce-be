namespace ECommerce.Products.Api.Application.Queries;
public class ProductListQuery : WorkListQuery
{
}

public class ProductListQueryValidator : AbstractValidator<ProductListQuery>
{
    public ProductListQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);
    }
}