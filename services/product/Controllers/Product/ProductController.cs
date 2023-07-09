using System.Diagnostics;
using AutoMapper;
using ECommerce.Shared.Common;
using ECommerce.Shared.Libs;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetProducts([FromQuery] ProductListQuery listQuery)
    {
        var query = _productRepository.Query;

        if (!string.IsNullOrEmpty(listQuery.keyword))
        {
            query = query.Where(FilterHelpers.BuildSearchPredicate<Product>(listQuery.keyword, new string[] { "Title", "Author", "PublicationYear", "CreatedAt" }));
        }

        query = query.OrderBy(p => p.CreatedAt);

        var sw = Stopwatch.StartNew();
        var list = await PaginationInfo.ToPaginatedListAsync(listQuery.Page, listQuery.PageSize, query);
        Console.WriteLine($"Performance test {sw.ElapsedMilliseconds}ms");
        sw.Stop();

        await PaginationInfo.AttachPaginationInfoToHeader(listQuery.Page, listQuery.PageSize, query);

        return Ok(list);
    }


}