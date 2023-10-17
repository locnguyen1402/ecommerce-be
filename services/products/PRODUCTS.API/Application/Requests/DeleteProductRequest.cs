namespace ECommerce.Products.Api.Application.Requests;
public class DeleteProductRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
}

public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, Guid>
{
    private readonly IProductRepository _productRepository;
    public DeleteProductRequestHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Guid> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindAsync(request.Id)
                        ?? throw new BaseException("Product not found", StatusCodes.Status404NotFound)
                        {
                            Title = nameof(NotFound),
                        };

        _productRepository.Remove(product);

        await _productRepository.SaveChangesAsync();

        return request.Id;
    }
}