using Application.Fetaures.BasketItems.Commands.Create;
using Application.Fetaures.BasketItems.Commands.Delete;
using Application.Fetaures.BasketItems.Commands.Update;
using Application.Fetaures.BasketItems.Queries.GetById;
using Application.Fetaures.BasketItems.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketItemsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBasketItemCommand command)
    {
        CreatedBasketItemResponse response = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBasketItemRequest request)
    {
        UpdatedBasketItemResponse response = await Mediator.Send(new UpdateBasketItemCommand { Id = id, Request=request });
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await Mediator.Send(new DeleteBasketItemCommand { Id = id });
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdBasketItemResponse response = await Mediator.Send(new GetByIdBasketItemQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var response = await Mediator.Send(new GetListBasketItemQuery());
        return Ok(response);
    }
}