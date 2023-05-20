using Domain.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController, ApiVersion("1.0")]
[Route("api/V{version:apiVersion}/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly ILogger<ToDoController> _logger;

    public ToDoController(ILogger<ToDoController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Post(OrderBook orderBook )
    {
        return Ok(orderBook);
    }

}