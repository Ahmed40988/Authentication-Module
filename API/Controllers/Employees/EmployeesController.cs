using Application.Commands.Employees;
using Application.DTO.Employees;
using Application.Queries.Employees;

namespace API.Controllers.Employees;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [HasPermission(Permissions.EmployeeView)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetAllEmployeesQuery());

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id}")]
    [HasPermission(Permissions.EmployeeView)]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(
            new GetEmployeeQuery(id));

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    [HasPermission(Permissions.EmployeeCreate)]
    public async Task<IActionResult> Create(
        [FromBody] EmployeeCreateRequestDto dto)
    {
        var command = new CreateEmployeeCommand(dto);

        var result = await _mediator.Send(command);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("assign")]
    [HasPermission(Permissions.EmployeeAssign)]
    public async Task<IActionResult> Assign(
        [FromBody] AssignEmployeeRequestDto dto)
    {
        var command =
            new AssignEmployeeCommand(dto);

        var result = await _mediator.Send(command);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id}")]
    [HasPermission(Permissions.EmployeeUpdate)]
    public async Task<IActionResult> Update(
        [FromRoute] string id,
        [FromBody] EmployeeUpdateRequestDto dto)
    {
        var command = new UpdateEmployeeCommand(id, dto);

        var result = await _mediator.Send(command);

        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    [HasPermission(Permissions.EmployeeDelete)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _mediator.Send(
            new DeleteEmployeeCommand(id));

        return StatusCode(result.StatusCode, result);
    }
}