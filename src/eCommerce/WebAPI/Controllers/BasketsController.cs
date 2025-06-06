using Application.Fetaures.Baskets.Commands.Create;
using Application.Fetaures.Baskets.Commands.Delete;
using Application.Fetaures.Baskets.Queries.GetById;
using Application.Fetaures.Baskets.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBasketCommand command)
    {
        var response = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await Mediator.Send(new DeleteBasketCommand (id));
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await Mediator.Send(new GetByIdBasketQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var response = await Mediator.Send(new GetListBasketQuery());
        return Ok(response);
    }
}