using AutoMapper;
using ECommerce.Shared.Common;
using ECommerce.Shared.Libs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Product;

public class ProductController : BaseController
{
    private readonly IProductRepository _productRepository;
    public ProductController(
            ILogger<ProductController> logger,
            IMapper mapper,
            IProductRepository productRepository
        ) : base(logger, mapper)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ProductItemResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts([FromQuery] ProductListQuery listQuery)
    {
        var query = _productRepository.Query.Include(p => p.ProductSaleInfo).AsQueryable();

        if (!string.IsNullOrEmpty(listQuery.keyword))
        {
            query = query.Where(FilterHelpers.BuildSearchPredicate<Product>(listQuery.keyword, new string[] { "Title", "Author", "PublicationYear", "CreatedAt" }));
        }

        query = query.OrderBy(p => p.CreatedAt);

        var list = await PaginatedList.ToListAsync(query, listQuery.Page, listQuery.PageSize);

        await PaginatedList.AttachToHeader(query, listQuery.Page, listQuery.PageSize);

        return Ok(_mapper.Map<List<ProductItemResponse>>(list));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDetailResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductDetail(Guid id)
    {
        var product = await _productRepository.GetProductDetail(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ProductDetailResponse>(product));
    }
}