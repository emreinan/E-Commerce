using Application.Fetaures.OrderItems.Commands.Create;
using Application.Fetaures.OrderItems.Commands.Delete;
using Application.Fetaures.OrderItems.Commands.Update;
using Application.Fetaures.OrderItems.Queries.GetById;
using Application.Fetaures.OrderItems.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderItemsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateOrderItemCommand command)
    {
        CreatedOrderItemResponse response = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateOrderItemRequest request)
    {
        UpdatedOrderItemResponse response = await Mediator.Send(new UpdateOrderItemCommand { Id = id, Request = request });
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await Mediator.Send(new DeleteOrderItemCommand { Id = id });
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdOrderItemResponse response = await Mediator.Send(new GetByIdOrderItemQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var response = await Mediator.Send(new GetListOrderItemQuery());
        return Ok(response);
    }
}
