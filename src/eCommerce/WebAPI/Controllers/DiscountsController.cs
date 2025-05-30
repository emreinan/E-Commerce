using Application.Fetaures.Discounts.Commands.Activate;
using Application.Fetaures.Discounts.Commands.Apply;
using Application.Fetaures.Discounts.Commands.Create;
using Application.Fetaures.Discounts.Commands.Deactivate;
using Application.Fetaures.Discounts.Commands.Delete;
using Application.Fetaures.Discounts.Commands.Update;
using Application.Fetaures.Discounts.Queries.GetByCode;
using Application.Fetaures.Discounts.Queries.GetById;
using Application.Fetaures.Discounts.Queries.GetList;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
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

    [HttpGet("code/{code}")]
    public async Task<IActionResult> GetByCode([FromRoute] string code)
    {
        GetByCodeDiscountQuery query = new() { Code = code };
        GetByCodeDiscountResponse response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpPost("apply")]
    public async Task<IActionResult> Apply([FromBody] ApplyDiscountCommand command)
    {
        AppliedDiscountResponse response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate([FromRoute] Guid id)
    {
        ActivateDiscountCommand command = new() { Id = id };
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate([FromRoute] Guid id)
    {
        DeactivateDiscountCommand command = new() { Id = id };
        await Mediator.Send(command);
        return NoContent();
    }
}
