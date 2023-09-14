using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Products.Api.Controllers;

public class ProductsBooksController : BaseController
{
    private readonly IBookRestClient _bookRestClient;
    public ProductsBooksController(
            ILogger<ProductsWorksController> logger,
            IMapper mapper,
            IBookRestClient bookRestClient
        ) : base(logger, mapper)
    {
        _bookRestClient = bookRestClient;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Work), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductBookDetail(string id)
    {
        var res = await _bookRestClient.GetBookDetail(id);

        if (res is null)
        {
            return NotFound();
        }

        return Ok(res);
    }
}