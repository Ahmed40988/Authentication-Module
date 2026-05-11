using Application.DTO.Employees;

namespace Application.Commands.Employees
{
    public record AssignEmployeeCommand(
        AssignEmployeeRequestDto Dto)
        : IRequest<Result<bool>>;
}