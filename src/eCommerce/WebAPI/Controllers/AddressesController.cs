using Application.Fetaures.Addresses.Commands.Create;
using Application.Fetaures.Addresses.Commands.Delete;
using Application.Fetaures.Addresses.Commands.Update;
using Application.Fetaures.Addresses.Queries.GetById;
using Application.Fetaures.Addresses.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAddressCommand command)
    {
        CreatedAddressResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAddressRequest request)
    {
        UpdatedAddressResponse response = await Mediator.Send(new UpdateAddressCommand { Id = id, Request = request });
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await Mediator.Send(new DeleteAddressCommand (id));
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAddressResponse response = await Mediator.Send(new GetByIdAddressQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var response = await Mediator.Send(new GetListAddressQuery());
        return Ok(response);
    }
}