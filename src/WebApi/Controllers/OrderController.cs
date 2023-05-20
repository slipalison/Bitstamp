using Domain.Commands;
using Domain.Models.AggregationOrder;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController, ApiVersion("1.0")]
[Route("api/V{version:apiVersion}/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ICorrelationContextService _correlation;
    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger, ICorrelationContextService correlationContextService)
    {
        _correlation = correlationContextService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok();
    }

    [HttpPost("buy")]
    public async Task<ActionResult> Post([FromBody] CreateOrder createOrder)
    {
        var t =  new Order
        {
            Id = _correlation.GetCorrelationId(),
            Amount = createOrder.Amount,
            Crypto = createOrder.TypeCripto,
            Price = 21.30m,
            AmountTotal = 0.99999m,
            Stock = new[] { new ItemOrder { Price = 27000.00m, Amount = 0.00669 } }, 
            Type = TypeOrder.Buy

        };
        
        return Ok(t);
    }

}
