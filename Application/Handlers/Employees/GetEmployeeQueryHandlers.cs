using Application.DTO.Employees;
using Application.Queries.Employees;
using Domain.Entities.Employees;

namespace Application.Handlers.Employees;

public class GetEmployeeQueryHandler(
    IGenericRepositories<Employee> repo,
    IStringLocalizer localizer
) : IRequestHandler<GetEmployeeQuery, Result<EmployeeResponseDto>>
{
    private readonly IGenericRepositories<Employee> repo = repo;
    private readonly IStringLocalizer localizer = localizer;

    public async Task<Result<EmployeeResponseDto>> Handle(
        GetEmployeeQuery request,
        CancellationToken cancellationToken)
    {
            var employee = await repo.Query()
                .Include(x => x.User)
                .Include(x => x.Department)
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (employee is null)
                return Result<EmployeeResponseDto>.Failure(
                    localizer["EmployeeNotFound"],
                    404);

            var dto = new EmployeeResponseDto(
                employee.Id,
                employee.User.FullName,
                employee.User.Email!,
                employee.User.PhoneNumber!,
                employee.JobTitle,
                employee.DepartmentId,
                employee.HireDate,
                 employee.Status.ToString()
            );

            return Result<EmployeeResponseDto>.Success(
                dto,
                localizer["Operationcompletedsuccessfully"]
            );
    }
}