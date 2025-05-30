using Application.Fetaures.OrderItems.Queries.GetById;
using Application.Fetaures.OrderItems.Queries.GetList;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderItemsController : BaseController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdOrderItemResponse response = await Mediator.Send(new GetByIdOrderItemQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var response = await Mediator.Send(new GetListOrderItemQuery());
        return Ok(response);
    }
}
