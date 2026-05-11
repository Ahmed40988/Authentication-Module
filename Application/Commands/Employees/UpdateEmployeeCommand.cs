using Application.DTO.Employees;
namespace Application.Commands.Employees { 
public record UpdateEmployeeCommand(
    string Id,
    EmployeeUpdateRequestDto? Dto
) : IRequest<Result<bool>>;
}