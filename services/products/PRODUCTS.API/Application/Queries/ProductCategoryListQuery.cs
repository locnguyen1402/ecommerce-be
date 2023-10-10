namespace ECommerce.Products.Api.Application.Queries;
public class ProductCategoryListQuery : PaginationQuery
{
    public string? Keyword { get; set; }
}

public class ProductCategoryListQueryValidator : AbstractValidator<ProductCategoryListQuery>
{
    public ProductCategoryListQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);
    }
}