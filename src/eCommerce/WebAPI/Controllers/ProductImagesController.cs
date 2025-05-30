using Application.Fetaures.ProductImages.Commands.Create;
using Application.Fetaures.ProductImages.Commands.Delete;
using Application.Fetaures.ProductImages.Commands.Update;
using Application.Fetaures.ProductImages.Queries.GetById;
using Application.Fetaures.ProductImages.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductImagesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] CreateProductImageCommand command)
    {
        List<CreatedProductImageResponse> response = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetList), response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateProductImageRequest request)
    {
        UpdatedProductImageResponse response = await Mediator.Send(new UpdateProductImageCommand { Id = id, Request = request });
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await Mediator.Send(new DeleteProductImageCommand { Id = id });
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdProductImageResponse response = await Mediator.Send(new GetByIdProductImageQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var response = await Mediator.Send(new GetListProductImageQuery());
        return Ok(response);
    }
}
