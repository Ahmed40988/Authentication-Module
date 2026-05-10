using Application.Commands.Products;
using Application.DTO.Products;
using Application.Queries.Products;

namespace API.Controllers.Products;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [HasPermission(Permissions.ProductsRead)]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetAllProductsQuery query)
    {
        var result = await _mediator.Send(query);

        return StatusCode(result.StatusCode, result);
    }


    [HttpGet("{id:guid}")]
    [HasPermission(Permissions.ProductsRead)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(
            new GetProductQuery(id)
        );

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    [HasPermission(Permissions.ProductsCreate)]
    public async Task<IActionResult> Create(
        [FromForm] ProductRequestDto dto)
    {
        var result = await _mediator.Send(
            new CreateProductCommand(dto)
        );

        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.ProductsUpdate)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromForm] ProductUpdateRequestDto dto)
    {
        var result = await _mediator.Send(
            new UpdateProductCommand(id, dto)
        );

        return StatusCode(result.StatusCode, result);
    }


    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.ProductsDelete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(
            new DeleteProductCommand(id)
        );

        return StatusCode(result.StatusCode, result);
    }


    [HttpPatch("{id:guid}/toggle-status")]
    [HasPermission(Permissions.ProductsUpdate)]
    public async Task<IActionResult> ToggleStatus(Guid id)
    {
        var result = await _mediator.Send(
            new ToggleProductStatusCommand(id)
        );

        return StatusCode(result.StatusCode, result);
    }
}