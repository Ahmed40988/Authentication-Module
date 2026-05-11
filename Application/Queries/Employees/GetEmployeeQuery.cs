using Application.DTO.Employees;
using Domain.Common;
using MediatR;

namespace Application.Queries.Employees
{
    public record GetEmployeeQuery(string Id) : IRequest<Result<EmployeeResponseDto>>;
}
