using System.Diagnostics;
using System.Net;
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
        if (
            requestInfo.OrderItems == null ||
            requestInfo.OrderItems.Count() == 0 ||
            requestInfo.OrderItems.GroupBy(x => x.ProductId).Any(x => x.Count() > 1)
            )
        {
            return BadRequest();
        }

        var response = await _productRestClient.GetProductInfos(requestInfo.OrderItems.Select(x => x.ProductId).ToList());

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var orderItems = response.Data!
                .Join
                (
                    requestInfo.OrderItems,
                    x => x.Id, x => x.ProductId,
                    (s1, s2) => new OrderItem
                    {
                        ProductId = s1.Id,
                        Title = s1.Title,
                        Quantity = s2.Quantity,
                        UnitPrice = s1.Price
                    }
                )
                .ToList();

            var order = new Order();
            order.AddOrderItems(orderItems);

            _orderRepository.Add(order);
            await _orderRepository.SaveChangesAsync();

            return Ok(response.Data);
        }

        return BadRequest();
    }
}