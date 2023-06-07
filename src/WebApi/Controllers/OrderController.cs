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
    private readonly ILogger<OrderController> _logger;

    public OrderController(IRequestOrderService requestOrderService, ILogger<OrderController> logger)
    {
        _requestOrderService = requestOrderService;
        _logger = logger;
    }

    [HttpPost("buy")]
    public async Task<ActionResult<Order>> PostBuy([FromBody] CreateOrder createOrder, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Testes");
        var result = await _requestOrderService.CreateAsync(createOrder, TypeOrder.Buy, cancellationToken);

        _logger.LogInformation("Testes2");
        if (result.IsSuccess)
            return Ok(result.Value);

        _logger.LogInformation("Testes3");

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