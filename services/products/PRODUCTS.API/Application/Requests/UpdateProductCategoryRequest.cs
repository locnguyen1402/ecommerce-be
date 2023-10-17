namespace ECommerce.Products.Api.Application.Requests;

public class UpdateProductCategoryRequest : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

public class UpdateProductCategoryRequestValidator : AbstractValidator<UpdateProductCategoryRequest>
{
    public UpdateProductCategoryRequestValidator(IProductCategoryRepository productCategoryRepository)
    {
        RuleFor(b => b.Id)
            .NotNull()
            .IsValidGuid();
        RuleFor(b => b.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(b => b.Description)
            .MaximumLength(250);
    }
}

public class UpdateProductCategoryRequestHandler : IRequestHandler<UpdateProductCategoryRequest, bool>
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    public UpdateProductCategoryRequestHandler(
        IProductCategoryRepository productCategoryRepository
    )
    {
        _productCategoryRepository = productCategoryRepository;
    }
    public async Task<bool> Handle(UpdateProductCategoryRequest request, CancellationToken cancellationToken)
    {
        var item = await _productCategoryRepository.FindAsync(request.Id);

        if (item is null)
        {
            throw new BaseException("Product category not found", StatusCodes.Status404NotFound)
            {
                Title = nameof(NotFound),
            };
        }

        var isExisted = await _productCategoryRepository.Query.AnyAsync(cate => cate.Id != request.Id && cate.Name == request.Name, cancellationToken);

        if (isExisted)
        {
            throw new BaseException("Category is existed", StatusCodes.Status400BadRequest)
            {
                Title = nameof(BadRequest),
            };
        }

        item.UpdateGeneralInfo(request.Name, request.Description);

        _productCategoryRepository.Update(item);

        await _productCategoryRepository.SaveChangesAsync();

        return true;
    }
}