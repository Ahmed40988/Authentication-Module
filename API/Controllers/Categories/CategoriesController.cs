using Application.Commands.Categories;
using Application.DTO.Categories;
using Application.Queries.Categories;

namespace API.Controllers.Categories
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [HasPermission(Permissions.CategoriesRead)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(
                new GetAllCategoriesQuery(pageNumber, pageSize)
            );

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.CategoriesRead)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(
                new GetCategoryQuery(id)
            );

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [HasPermission(Permissions.CategoriesCreate)]
        public async Task<IActionResult> Create(
            [FromBody] CategoryRequestDTo dto)
        {
            var command = new CreateCategoryCommand(dto);

            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.CategoriesUpdate)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] CategoryRequestDTo dto)
        {
            var command = new UpdateCategoryCommand(id, dto);
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.CategoriesDelete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(
                new DeleteCategoryCommand(id)
            );

            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{id:guid}/toggle-status")]
        [HasPermission(Permissions.CategoriesUpdate)]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            var result = await _mediator.Send(
                new ToggleCategoryStatusCommand(id)
            );

            return StatusCode(result.StatusCode, result);
        }
    }
}