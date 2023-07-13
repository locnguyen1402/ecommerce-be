using System.Diagnostics;
using AutoMapper;
using ECommerce.Services.Orders;
using ECommerce.Shared.Common;
using ECommerce.Shared.Libs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Orders;

public class OrderController : BaseController
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRestClient _productRestClient;
    public OrderController(
            ILogger<OrderController> logger,
            IMapper mapper,
            IOrderRepository orderRepository,
            IProductRestClient productRestClient
        ) : base(logger, mapper)
    {
        _orderRepository = orderRepository;
        _productRestClient = productRestClient;
    }

    [HttpGet("products/{id:guid}")]
    public async Task<IActionResult> GetProductDetail(Guid id)
    {
        var product = await _productRestClient.GetProductInfo(id);

        return Ok(product);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderDetail(Guid id)
    {
        var order = await _orderRepository.GetOrderDetail(id);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest requestInfo)
    {
        if (requestInfo.OrderItems == null || requestInfo.OrderItems.Count() == 0)
        {
            return BadRequest();
        }

        var orderItems = requestInfo.OrderItems
                .Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList();

        var order = new Order();
        order.AddOrderItems(orderItems);

        _orderRepository.Add(order);
        await _orderRepository.SaveChangesAsync();

        return Ok(order);
    }
}