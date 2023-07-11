using AutoMapper;
using ECommerce.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Product;

public class ProductSaleController : BaseController
{
    private readonly IProductRepository _productRepository;
    public ProductSaleController(
            ILogger<ProductController> logger,
            IMapper mapper,
            IProductRepository productRepository
        ) : base(logger, mapper)
    {
        _productRepository = productRepository;
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductDetailResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EditProductSaleInfo(Guid id, [FromBody] EditProductSaleInfoRequest requestInfo)
    {
        var product = await _productRepository.GetProductDetail(id);

        if (product == null)
        {
            return NotFound();
        }

        product.AddSaleInfo(requestInfo.Quantity, requestInfo.Price);

        _productRepository.Update(product);

        await _productRepository.SaveChangesAsync();

        return Ok(_mapper.Map<ProductDetailResponse>(product));
    }
}