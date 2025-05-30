using Application.Fetaures.Orders.Commands.Create;
using Application.Fetaures.Orders.Queries.GetById;
using Application.Fetaures.Orders.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateOrderCommand command)
    {
        CreatedOrderResponse response = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdOrderResponse response = await Mediator.Send(new GetByIdOrderQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var response = await Mediator.Send(new GetListOrderQuery());
        return Ok(response);
    }
}
