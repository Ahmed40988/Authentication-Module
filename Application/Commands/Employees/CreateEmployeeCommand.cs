using Application.DTO.Employees;
using Application.Interfaces;
using Domain.Common;
using MediatR;

namespace Application.Commands.Employees
{
    public record CreateEmployeeCommand(EmployeeCreateRequestDto Dto) : IRequest<Result<string>>;
}
