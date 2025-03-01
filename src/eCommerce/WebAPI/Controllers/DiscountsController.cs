using Application.Fetaures.Discounts.Commands.Create;
using Application.Fetaures.Discounts.Commands.Delete;
using Application.Fetaures.Discounts.Commands.Update;
using Application.Fetaures.Discounts.Queries.GetById;
using Application.Fetaures.Discounts.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateDiscountCommand command)
    {
        CreatedDiscountResponse response = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDiscountRequest request)
    {
        UpdatedDiscountResponse response = await Mediator.Send(new UpdateDiscountCommand { Id = id, Request = request });
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await Mediator.Send(new DeleteDiscountCommand { Id = id });
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdDiscountResponse response = await Mediator.Send(new GetByIdDiscountQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var response = await Mediator.Send(new GetListDiscountQuery());
        return Ok(response);
    }
}
