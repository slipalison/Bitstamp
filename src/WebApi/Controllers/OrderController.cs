using Domain.Commands;
using Domain.Contracts.Services;
using Domain.Models.AggregationOrder;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController, ApiVersion("1.0")]
[Route("api/V{version:apiVersion}/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IRequestOrderService _requestOrderService;
    private readonly ICorrelationContextService _correlation;
    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger, ICorrelationContextService correlationContextService,
        IRequestOrderService requestOrderService)
    {
        _requestOrderService = requestOrderService;
        _correlation = correlationContextService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok();
    }

    [HttpPost("buy")]
    public async Task<ActionResult> PostBuy([FromBody] CreateOrder createOrder, CancellationToken cancellationToken)
    {
        var result = await _requestOrderService.CreateAsync(createOrder, TypeOrder.Buy, cancellationToken);

        return Ok(result);
    }


    [HttpPost("sell")]
    public async Task<ActionResult> PostSell([FromBody] CreateOrder createOrder, CancellationToken cancellationToken)
    {
        var result = await _requestOrderService.CreateAsync(createOrder, TypeOrder.Sell, cancellationToken);

        return Ok(result);
    }
}
