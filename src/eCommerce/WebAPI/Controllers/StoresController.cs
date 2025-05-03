using Application.Fetaures.Stores.Commands.Create;
using Application.Fetaures.Stores.Commands.Delete;
using Application.Fetaures.Stores.Commands.Update;
using Application.Fetaures.Stores.Queries.GetById;
using Application.Fetaures.Stores.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoresController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStoreCommand command)
    {
        CreatedStoreResponse response = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStoreRequest request)
    {
        UpdatedStoreResponse response = await Mediator.Send(new UpdateStoreCommand { Id = id, Request = request });
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await Mediator.Send(new DeleteStoreCommand { Id = id });
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStoreResponse response = await Mediator.Send(new GetByIdStoreQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var response = await Mediator.Send(new GetListStoreQuery());
        return Ok(response);
    }
}
