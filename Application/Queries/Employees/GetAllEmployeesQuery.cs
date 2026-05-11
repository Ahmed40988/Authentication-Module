using Application.DTO.Employees;
using Domain.Enums;

namespace Application.Queries.Employees;

public record GetAllEmployeesQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string? Search = null,
    EmployeeStatus? Status = null,
    Guid? DepartmentId = null
) : IRequest<Result<PaginatedList<EmployeeListItemDto>>>;