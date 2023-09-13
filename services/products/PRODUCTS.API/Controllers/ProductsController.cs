

using ECommerce.Shared.Integration.Application.Queries;

namespace ECommerce.Products.Api.Controllers;

public class ProductsController : BaseController
{
    private readonly IProductRepository _productRepository;
    private readonly IWorkRestClient _bookRestClient;
    public ProductsController(
            ILogger<ProductsController> logger,
            IMapper mapper,
            IProductRepository productRepository,
            IWorkRestClient bookRestClient
        ) : base(logger, mapper)
    {
        _productRepository = productRepository;
        _bookRestClient = bookRestClient;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Work>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts(WorkListQuery query)
    {
        var res = await _bookRestClient.GetWorks(query);

        return Ok(res);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Work), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct(string id)
    {
        var res = await _bookRestClient.GetWorkDetail(id);

        if(res is null) {
            return NotFound();
        }

        return Ok(res);
    }
}