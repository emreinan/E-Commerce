using Application.Fetaures.Users.Commands.Create;
using Application.Fetaures.Users.Commands.Delete;
using Application.Fetaures.Users.Commands.Update;
using Application.Fetaures.Users.Queries.GetById;
using Application.Fetaures.Users.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid Id)
    {
        GetByIdUserResponse result = await Mediator.Send(new GetByIdUserQuery { Id = Id });
        return Ok(result);
    }

    [HttpGet("GetFromAuth")]
    public async Task<IActionResult> GetFromAuth()
    {
        GetByIdUserQuery getByIdUserQuery = new() { Id = GetUserIdFromRequest() };
        GetByIdUserResponse result = await Mediator.Send(getByIdUserQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await Mediator.Send(new GetListUserQuery());
        return Ok(result);
    }
    // Only Admin can access this endpoint
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] CreateUserCommand createUserCommand)
    {
        CreatedUserResponse result = await Mediator.Send(createUserCommand);
        return Created(uri: "", result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateUserRequest request)
    {
        UpdatedUserResponse result = await Mediator.Send(new UpdateUserCommand { Id = id, Request = request });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await Mediator.Send(new DeleteUserCommand {Id = id});
        return NoContent();
    }
}
