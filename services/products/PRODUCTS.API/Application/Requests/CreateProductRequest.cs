namespace ECommerce.Products.Api.Application.Requests;

public class CreateProductRequest : IRequest<Product>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Price { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    public Guid CategoryId { get; set; }
}

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator(IProductCategoryRepository productCategoryRepository)
    {
        RuleFor(b => b.Name)
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
    public CreateProductRequestHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Product> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Description);
        product.AddTags(request.Tags);
        product.AssignToCategory(request.CategoryId);

        _productRepository.Add(product);

        await _productRepository.SaveChangesAsync();

        await _productRepository.CreateEntry(product).Reference(p => p.ProductCategory).LoadAsync(cancellationToken);

        return product;
    }
}