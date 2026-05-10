using Application.Commands.Categories;
using Application.DTO.Categories;
using Application.Queries.Categories;

namespace API.Controllers.Categories
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoriesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [HasPermission(Permissions.SubCategoriesRead)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(
                new GetAllSubCategoriesQuery(pageNumber, pageSize)
            );

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.SubCategoriesRead)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(
                new GetSubCategoryQuery(id)
            );

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
    [HasPermission(Permissions.SubCategoriesCreate)]
    public async Task<IActionResult> Create(
        [FromBody] SubCategoryRequestDto dto)
    {
        var command = new CreateSubCategoryCommand(dto);

        var result = await _mediator.Send(command);

        return StatusCode(result.StatusCode, result);
    }

        /// <summary>
        /// Update sub category.
        /// </summary>
        /// <remarks>
        /// categoryId:
        /// - null => keep current category.
        /// - valid Guid => change category.
        /// - empty string "" is not allowed.
        /// </remarks>
        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.SubCategoriesUpdate)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] SubCategoryUpdateRequestDto dto)
        {
            var command = new UpdateSubCategoryCommand(id, dto);

            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.SubCategoriesDelete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(
                new DeleteSubCategoryCommand(id)
            );

            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{id:guid}/toggle-status")]
        [HasPermission(Permissions.SubCategoriesUpdate)]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            var result = await _mediator.Send(
                new ToggleSubCategoryStatusCommand(id)
            );

            return StatusCode(result.StatusCode, result);
        }
    }
}