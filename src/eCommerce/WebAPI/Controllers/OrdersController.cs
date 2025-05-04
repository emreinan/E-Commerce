using Application.Fetaures.Orders.Commands.Create;
using Application.Fetaures.Orders.Commands.Delete;
using Application.Fetaures.Orders.Commands.Update;
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateOrderRequest request)
    {
        UpdatedOrderResponse response = await Mediator.Send(new UpdateOrderCommand { Id = id, Request = request });
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await Mediator.Send(new DeleteOrderCommand { Id = id });
        return NoContent();
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
