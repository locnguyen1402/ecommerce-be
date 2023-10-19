namespace ECommerce.Products.Api.Application.Requests;

public class CreateProductRequest : IRequest<Guid>
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int Price { get; set; }
    public List<Guid>? TagIds { get; set; } = new();
    public List<string>? Tags { get; set; } = new();
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

public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ICategoryRepository _categoryRepository;
    public CreateProductRequestHandler(
        IProductRepository productRepository,
        ITagRepository tagRepository,
        ICategoryRepository categoryRepository
    )
    {
        _productRepository = productRepository;
        _tagRepository = tagRepository;
        _categoryRepository = categoryRepository;
    }
    public async Task<Guid> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        if (!await _categoryRepository.IsExisted(request.CategoryId))
        {
            throw new BaseException("Product category not found", StatusCodes.Status404NotFound)
            {
                Title = nameof(NotFound),
            };
        }

        var product = new Product(request.Title, request.Description);

        var tags = new List<Tag>();

        if (!request.TagIds.IsNullOrEmpty())
        {
            tags = await _tagRepository.Query.Where(t => request.TagIds!.Contains(t.Id)).ToListAsync(cancellationToken);
        }
        else if (!request.Tags.IsNullOrEmpty())
        {
            tags = await _tagRepository.Query.Where(t => request.Tags!.Contains(t.Value)).ToListAsync(cancellationToken);
        }
        product.AddTags(tags);

        product.ChangePrice(request.Price);
        product.AssignToCategory(request.CategoryId);

        _productRepository.Add(product);

        await _productRepository.SaveChangesAsync();

        return product.Id;
    }
}