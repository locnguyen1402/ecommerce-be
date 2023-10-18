namespace ECommerce.Products.Api.Application.Requests;

public class UpdateProductRequest : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int Price { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
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
    private readonly ICategoryRepository _productCategoryRepository;
    public UpdateProductRequestHandler(
        IProductRepository productRepository,
        ICategoryRepository productCategoryRepository
    )
    {
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
    }
    public async Task<bool> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var category = await _productCategoryRepository.FindAsync(request.CategoryId);

        if (category is null)
        {
            throw new BaseException("Product category not found", StatusCodes.Status404NotFound)
            {
                Title = nameof(NotFound),
            };
        }

        var product = await _productRepository.FindAsync(request.Id);

        if (product is null)
        {
            throw new BaseException("Product not found", StatusCodes.Status404NotFound)
            {
                Title = nameof(NotFound),
            };
        }

        product.UpdateGeneralInfo(request.Title, request.Description);
        product.ChangePrice(request.Price);
        // product.AddTags(request.Tags);
        product.AssignToCategory(request.CategoryId);

        _productRepository.Update(product);

        await _productRepository.SaveChangesAsync();

        return true;
    }
}