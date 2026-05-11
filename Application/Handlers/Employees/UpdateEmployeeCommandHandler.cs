using Application.Commands.Employees;
using Domain.Entities.Departments;
using Domain.Entities.Employees;

namespace Application.Handlers.Employees;

public class UpdateEmployeeCommandHandler(
    IGenericRepositories<Employee> repo,
    IGenericRepositories<Department> departmentRepo,
    IStringLocalizer localizer
) : IRequestHandler<UpdateEmployeeCommand, Result<bool>>
{
    private readonly IGenericRepositories<Employee> repo = repo;
    private readonly IGenericRepositories<Department> departmentRepo = departmentRepo;
    private readonly IStringLocalizer localizer = localizer;

    public async Task<Result<bool>> Handle(
        UpdateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var employee = await repo.Query()
            .FirstOrDefaultAsync(
                x => x.Id == request.Id,
                cancellationToken);

        if (employee is null)
            return Result<bool>.Failure(
                localizer["EmployeeNotFound"],
                404);

        if (dto.DepartmentId.HasValue)
        {
            var departmentExists = await departmentRepo.AnyAsync(
                x => x.Id == dto.DepartmentId.Value);

            if (!departmentExists)
                return Result<bool>.Failure(
                    localizer["DepartmentNotFound"],
                    404);
        }

        var jobTitle = dto.JobTitle?.Trim()?? employee.JobTitle;
        var departmentId = dto.DepartmentId ?? employee.DepartmentId;
        var hireDate =   dto.HireDate?? employee.HireDate;

        var status = dto.Status?? employee.Status;

        var hireDateResult = employee.UpdateHireDate(hireDate);

        if (!hireDateResult.IsSuccess)
            return Result<bool>.Failure(
                localizer[hireDateResult.Message!],
                hireDateResult.StatusCode);

        var updateResult = employee.Update(
                jobTitle,
                departmentId,
                status
            );
        await repo.UpdateAsync(
            employee,
            cancellationToken);

        return Result<bool>.Success(
            true,
            localizer["Operationcompletedsuccessfully"]
        );
    }
}