namespace ECommerce.Products.Api.Controllers;

public class ProductsController : BaseController
{
    private readonly IProductRepository _productRepository;
    public ProductsController(
            ILogger<ProductsController> logger,
            IMapper mapper,
            IProductRepository productRepository
        ) : base(logger, mapper)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    // [ProducesResponseType(typeof(List<ProductItemResponse>), StatusCodes.Status200OK)]
    public IActionResult GetProducts()
    {
        // var query = _productRepository.Query.Include(p => p.ProductSaleInfo).AsQueryable();

        // if (!string.IsNullOrEmpty(listQuery.keyword))
        // {
        //     query = query.Where(FilterHelpers.BuildSearchPredicate<Product>(listQuery.keyword, new string[] { "Title", "Author", "PublicationYear", "CreatedAt" }));
        // }

        // query = query.OrderBy(p => p.CreatedAt);

        // var list = await PaginatedList.ToListAsync(query, listQuery.Page, listQuery.PageSize);

        // await PaginatedList.AttachToHeader(query, listQuery.Page, listQuery.PageSize);

        // return Ok(_mapper.Map<List<ProductItemResponse>>(list));

        return Ok();
    }

    // [HttpGet("{id:guid}")]
    // [ProducesResponseType(typeof(ProductDetailResponse), StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<IActionResult> GetProductDetail(Guid id)
    // {
    //     var product = await _productRepository.GetProductDetail(id);

    //     if (product == null)
    //     {
    //         return NotFound();
    //     }

    //     return Ok(_mapper.Map<ProductDetailResponse>(product));
    // }

    // [HttpPost("order")]
    // public async Task<IActionResult> GetProductDetail([FromBody] OrderProductsRequest requestInfo)
    // {
    //     if (requestInfo.Ids.Count() == 0)
    //     {
    //         return BadRequest();
    //     }

    //     var products = await _productRepository.Query
    //                             .Include(p => p.ProductSaleInfo)
    //                             .Where(x => requestInfo.Ids.Contains(x.Id)).ToListAsync();

    //     if (products.Count != requestInfo.Ids.Count)
    //     {
    //         return BadRequest();
    //     }

    //     return Ok(_mapper.Map<List<ProductItemResponse>>(products));
    // }
}