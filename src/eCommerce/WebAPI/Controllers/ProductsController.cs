using Application.Fetaures.Products.Commands.Create;
using Application.Fetaures.Products.Commands.Delete;
using Application.Fetaures.Products.Commands.Update;
using Application.Fetaures.Products.Queries.GetById;
using Application.Fetaures.Products.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateProductCommand command)
    {
        CreatedProductResponse response = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductRequest request)
    {
        UpdatedProductResponse response = await Mediator.Send(new UpdateProductCommand { Id = id, Request = request });
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await Mediator.Send(new DeleteProductCommand { Id = id });
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdProductResponse response = await Mediator.Send(new GetByIdProductQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var response = await Mediator.Send(new GetListProductQuery());
        return Ok(response);
    }
}
