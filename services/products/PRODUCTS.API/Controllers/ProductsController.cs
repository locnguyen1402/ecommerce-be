

using ECommerce.Shared.Integration.Application.Queries;

namespace ECommerce.Products.Api.Controllers;

public class ProductsController : BaseController
{
    private readonly IProductRepository _productRepository;
    private IWorkRestClient _bookRestClient;
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
    public async Task<IActionResult> GetProduct()
    {
        var res = await _bookRestClient.GetWorks(new WorkListQuery());

        return Ok(res);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Work), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts(string id)
    {
        var res = await _bookRestClient.GetWorkDetail(id);

        return Ok(res);
    }
}