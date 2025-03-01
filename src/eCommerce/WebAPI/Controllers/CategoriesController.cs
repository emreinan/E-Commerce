using Application.Fetaures.Categories.Commands.Create;
using Application.Fetaures.Categories.Commands.Delete;
using Application.Fetaures.Categories.Commands.Update;
using Application.Fetaures.Categories.Queries.GetById;
using Application.Fetaures.Categories.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCategoryCommand command)
    {
        var response = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromRoute] Guid id ,[FromBody] UpdateCategoryRequest request)
    {
        var response = await Mediator.Send(new UpdateCategoryCommand { Id = id, Request = request });
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await Mediator.Send(new DeleteCategoryCommand { Id = id });
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await Mediator.Send(new GetByIdCategoryQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var response = await Mediator.Send(new GetListCategoryQuery());
        return Ok(response);
    }
}