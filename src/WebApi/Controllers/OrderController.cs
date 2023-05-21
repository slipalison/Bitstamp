using Domain.Commands;
using Domain.Contracts.Services;
using Domain.Models.AggregationOrder;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController, ApiVersion("1.0")]
[Route("api/V{version:apiVersion}/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IRequestOrderService _requestOrderService;

    public OrderController(IRequestOrderService requestOrderService)
    {
        _requestOrderService = requestOrderService;
    }

    [HttpPost("buy")]
    public async Task<ActionResult<Order>> PostBuy([FromBody] CreateOrder createOrder, CancellationToken cancellationToken)
    {
        var result = await _requestOrderService.CreateAsync(createOrder, TypeOrder.Buy, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }

    [HttpPost("sell")]
    public async Task<ActionResult<Order>> PostSell([FromBody] CreateOrder createOrder, CancellationToken cancellationToken)
    {
        var result = await _requestOrderService.CreateAsync(createOrder, TypeOrder.Sell, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }
}