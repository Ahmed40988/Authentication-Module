using Application.Commands.Categories;
using Application.DTO.Categories;
using Application.Queries.Categories;

namespace API.Controllers.Categories
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubSubCategoriesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [HasPermission(Permissions.SubSubCategoriesRead)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(
                new GetAllSubSubCategoriesQuery(pageNumber, pageSize)
            );

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id:guid}")]
        [HasPermission(Permissions.SubSubCategoriesRead)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(
                new GetSubSubCategoryQuery(id)
            );

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [HasPermission(Permissions.SubSubCategoriesCreate)]
        public async Task<IActionResult> Create(
            [FromBody] SubSubCategoryRequestDto dto)
        {
            var command = new CreateSubSubCategoryCommand(dto);

            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }
        /// <summary>
        /// Update subsubcategory.
        /// </summary>
        /// <remarks>
        /// subcategoryId:
        /// - null => keep current subcategory.
        /// - valid Guid => change subcategory.
        /// - empty string "" is not allowed.
        /// </remarks>
        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.SubSubCategoriesUpdate)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] SubSubCategoryUpdateRequestDto dto)
        {
            var command = new UpdateSubSubCategoryCommand(id, dto);
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.SubSubCategoriesDelete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(
            new DeleteSubSubCategoryCommand(id) );
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{id:guid}/toggle-status")]
        [HasPermission(Permissions.SubSubCategoriesUpdate)]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            var result = await _mediator.Send(
            new ToggleSubSubCategoryStatusCommand(id));
          return StatusCode(result.StatusCode, result);
        }

    }
}