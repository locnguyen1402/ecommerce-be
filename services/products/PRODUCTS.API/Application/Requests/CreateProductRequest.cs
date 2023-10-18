namespace ECommerce.Products.Api.Application.Requests;

public class CreateProductRequest : IRequest<Product>
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int Price { get; set; }
    public List<Guid>? Tags { get; set; } = new List<Guid>();
    public Guid CategoryId { get; set; }
}

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(b => b.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(b => b.Description)
            .MaximumLength(250);
        RuleFor(b => b.Price)
            .GreaterThan(0);
    }
}

public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, Product>
{
    private readonly IProductRepository _productRepository;
    private readonly ITagRepository _tagRepository;
    public CreateProductRequestHandler(
        IProductRepository productRepository,
        ITagRepository tagRepository
    )
    {
        _productRepository = productRepository;
        _tagRepository = tagRepository;
    }
    public async Task<Product> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Title, request.Description);

        if (!request.Tags.IsNullOrEmpty())
        {
            var tags = await _tagRepository.Query.Where(t => request.Tags!.Contains(t.Id)).ToListAsync(cancellationToken);
            product.AddTags(tags);
        }

        product.AssignToCategory(request.CategoryId);

        _productRepository.Add(product);

        await _productRepository.SaveChangesAsync();

        await _productRepository.CreateEntry(product).Reference(p => p.Category).LoadAsync(cancellationToken);

        return product;
    }
}