using Application.Commands.Brands;
using Application.DTOs.Brands;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Queries.Brands;

namespace API.Controllers.Catalog
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [HasPermission(Permissions.BrandsRead)]
        public async Task<IActionResult> GetAll([FromQuery] int PageNumber,int PageSize)
        {
            var result = await _mediator.Send(new GetAllBrandsQuery(PageNumber,PageSize));
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.BrandsRead)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetBrandQuery(id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [HasPermission(Permissions.BrandsCreate)]
        public async Task<IActionResult> Create([FromForm] CreateBrandCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{Id:guid}")]
        [HasPermission(Permissions.BrandsUpdate)]
        public async Task<IActionResult> Update([FromRoute]Guid Id ,[FromForm] UpdateBrandDto dto)
        {
            var command = new UpdateBrandCommand(Id, dto);
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.BrandsDelete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteBrandCommand(id));
            return StatusCode(result.StatusCode, result);
        }


        [HttpPatch("{id:guid}/toggle-status")]
        [HasPermission(Permissions.BrandsUpdate)]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            var result = await _mediator.Send(new ToggleBrandStatusCommand(id));
            return StatusCode(result.StatusCode, result);
        }
    }
}