namespace ECommerce.Products.Api.Controllers;
public class ProductsController : BaseController
{
    private readonly IProductRepository _productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly IMediator _mediator;
    public ProductsController(
            ILogger<ProductsController> logger,
            IMapper mapper,
            IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository,
            IMediator mediator
        ) : base(logger, mapper)
    {
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ProductItemResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts([FromQuery] ProductListQuery queryRes)
    {
        var query = _productRepository.Query.Include(p => p.ProductCategory).OrderBy(p => p.CreatedAt).AsQueryable();

        if (!queryRes.Keyword.IsNullOrEmpty())
        {
            query = query.Where(p => p.Name.Contains(queryRes.Keyword!));
        }

        var result = await PaginatedList<Product>.CreateFromQuery(query, queryRes.Page, queryRes.PageSize);

        result.ExposeHeader();

        return Ok(_mapper.Map<List<Product>, List<ProductItemResponse>>(result.Items));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        if (!await _productCategoryRepository.IsExisted(request.CategoryId))
        {
            return BadRequest("Category not found");
        }

        var result = await _mediator.Send(request);

        return Ok(_mapper.Map<Product, ProductDetailResponse>(result));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDetailResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductDetail(Guid id)
    {
        var query = _productRepository.Query.Include(p => p.ProductCategory);

        var result = await query.FirstOrDefaultAsync(p => p.Id == id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<Product, ProductDetailResponse>(result));
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProductDetail(Guid id, [FromBody] UpdateProductRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(request);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        return Ok(await _mediator.Send(new DeleteProductRequest { Id = id }));
    }
}