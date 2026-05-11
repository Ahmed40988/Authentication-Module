using Application.DTO.Employees;
using Application.Queries.Employees;
using Domain.Entities.Employees;

namespace Application.Handlers.Employees
{ 
public class GetAllEmployeesQueryHandler(
    IGenericRepositories<Employee> repo,
    IStringLocalizer localizer
) : IRequestHandler<
    GetAllEmployeesQuery,
    Result<PaginatedList<EmployeeListItemDto>>>
{
    private readonly IGenericRepositories<Employee> repo = repo;
    private readonly IStringLocalizer localizer = localizer;

    public async Task<
        Result<PaginatedList<EmployeeListItemDto>>> Handle(
        GetAllEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var query = repo.Query()
            .Include(x => x.User)
            .Include(x => x.Department)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(x =>
                x.User.FullName.Contains(request.Search));

        if (request.Status.HasValue)
            query = query.Where(x =>
                x.Status == request.Status.Value);

        if (request.DepartmentId.HasValue)
            query = query.Where(x =>
                x.DepartmentId == request.DepartmentId.Value);


        var employeesQuery = query
            .Select(employee => new EmployeeListItemDto(
                employee.Id,
                employee.User.FullName,
                employee.User.Email!,
                employee.JobTitle,
                employee.Department.Name.En,
                  employee.Status.ToString()
            ));

        var paginatedEmployees =
            await PaginatedList<EmployeeListItemDto>
            .CreateAsync(
                employeesQuery,
                request.PageNumber,
                request.PageSize
            );

        return Result<PaginatedList<EmployeeListItemDto>>
            .Success(
                paginatedEmployees,
                localizer["Operationcompletedsuccessfully"]
            );
    }
}
}