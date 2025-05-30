using Application.Fetaures.ProductComments.Commands.Create;
using Application.Fetaures.ProductComments.Commands.Delete;
using Application.Fetaures.ProductComments.Commands.Update;
using Application.Fetaures.ProductComments.Queries.GetById;
using Application.Fetaures.ProductComments.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductCommentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateProductCommentCommand command)
    {
        CreatedProductCommentResponse response = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductCommentRequest request)
    {
        UpdatedProductCommentResponse response = await Mediator.Send(new UpdateProductCommentCommand { Id = id, Request = request });
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await Mediator.Send(new DeleteProductCommentCommand { Id = id });
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdProductCommentResponse response = await Mediator.Send(new GetByIdProductCommentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var response = await Mediator.Send(new GetListProductCommentQuery());
        return Ok(response);
    }
}