namespace ECommerce.Products.Api.Application.Requests;

public class UpdateProductRequest : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int Price { get; set; }
    public List<Guid> Tags { get; set; } = new List<Guid>();
    public Guid CategoryId { get; set; }
}

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(b => b.Id)
            .NotNull()
            .IsValidGuid();
        RuleFor(b => b.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(b => b.Description)
            .MaximumLength(250);
        RuleFor(b => b.Price)
            .GreaterThan(0);
        RuleFor(b => b.CategoryId)
            .NotNull()
            .IsValidGuid();
    }
}

public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, bool>
{
    private readonly IProductRepository _productRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ICategoryRepository _productCategoryRepository;
    public UpdateProductRequestHandler(
        IProductRepository productRepository,
        ITagRepository tagRepository,
        ICategoryRepository productCategoryRepository
    )
    {
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
    }
    public async Task<bool> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var category = await _productCategoryRepository.FindAsync(request.CategoryId) ?? throw new BaseException("Product category not found", StatusCodes.Status404NotFound)
        {
            Title = nameof(NotFound),
        };

        var product = await _productRepository.FindAsync(request.Id) ?? throw new BaseException("Product not found", StatusCodes.Status404NotFound)
        {
            Title = nameof(NotFound),
        };

        product.UpdateGeneralInfo(request.Title, request.Description);
        product.ChangePrice(request.Price);
        product.AssignToCategory(request.CategoryId);

        var tags = await _tagRepository.Query.Where(t => request.Tags!.Contains(t.Id)).ToListAsync(cancellationToken);
        product.AddTags(tags);

        _productRepository.Update(product);

        await _productRepository.SaveChangesAsync();

        return true;
    }
}