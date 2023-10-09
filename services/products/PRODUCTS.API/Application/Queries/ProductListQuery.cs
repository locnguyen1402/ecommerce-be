namespace ECommerce.Products.Api.Application.Queries;
public class ProductListQuery : PaginationQuery
{
    public string? Keyword { get; set; }
}

public class ProductListQueryValidator : AbstractValidator<ProductListQuery>
{
    public ProductListQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);
    }
}