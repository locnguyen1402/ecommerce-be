namespace ECommerce.Products.Api.Application.Queries;
public class ProductListQuery : PaginationQuery, IRequest<PaginatedList<Product>>
{
    public string? Keyword { get; set; }
    public Guid? CategoryId { get; set; }
    public List<Guid>? CategoryIds { get; set; }
    public List<string>? Tags { get; set; }
}

public class ProductListQueryValidator : AbstractValidator<ProductListQuery>
{
    public ProductListQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);
    }
}

public class ProductListQueryHandler : IRequestHandler<ProductListQuery, PaginatedList<Product>>
{
    private readonly IProductRepository _productRepository;
    public ProductListQueryHandler(
        IProductRepository productRepository
    )
    {
        _productRepository = productRepository;
    }
    public async Task<PaginatedList<Product>> Handle(ProductListQuery request, CancellationToken cancellationToken)
    {
        var query = _productRepository.Query.Include(p => p.Category).OrderBy(p => p.CreatedAt).AsQueryable();

        if (!request.Keyword.IsNullOrEmpty())
        {
            query = query.Where(p => p.Title.Contains(request.Keyword!));
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == request.CategoryId);
        }
        else if (!request.CategoryIds.IsNullOrEmpty())
        {
            query = query.Where(p => request.CategoryIds!.Contains(p.CategoryId));
        }

        var result = await PaginatedList<Product>.CreateFromQuery(query, request.Page, request.PageSize);

        // if (!request.Tags.IsNullOrEmpty())
        // {
        //     var items = result.Items.Where(p => request.Tags!.Any(t => p.Tags.Contains(t))).ToList();

        //     result = PaginatedList<Product>.CreateFromList(items, request.Page, request.PageSize);
        // }

        return result;
    }
}