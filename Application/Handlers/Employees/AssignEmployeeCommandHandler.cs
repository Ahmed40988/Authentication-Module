using Application.Commands.Employees;
using Domain.Entities.Departments;
using Domain.Entities.Employees;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers.Employees;

public class AssignEmployeeCommandHandler(
    IGenericRepositories<Employee> repo,
    IGenericRepositories<Department> departmentRepo,
    UserManager<User> userManager,
    IStringLocalizer localizer
) : IRequestHandler<
    AssignEmployeeCommand,
    Result<bool>>
{
    private readonly IGenericRepositories<Employee> repo = repo;
    private readonly IGenericRepositories<Department> departmentRepo = departmentRepo;
    private readonly UserManager<User> userManager = userManager;
    private readonly IStringLocalizer localizer = localizer;

    public async Task<Result<bool>> Handle(
        AssignEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var dto = request.Dto;
            var user = await userManager.FindByIdAsync(dto.UserId);

            if (user is null)
                return Result<bool>.Failure(localizer["UserNotFound"],404);

            var employeeExists = await repo.AnyAsync(x => x.Id == user.Id);

            if (employeeExists)
                return Result<bool>.Failure(localizer["EmployeeAlreadyExists"], 400);

            var department = await departmentRepo.GetByGuidIdAsync(dto.DepartmentId);

            if (department is null)
                return Result<bool>.Failure(
                    localizer["DepartmentNotFound"],
                    404);

            if (!await userManager.IsInRoleAsync(user, "Employee"))
                await userManager.AddToRoleAsync(user, "Employee");

            var employee = new Employee(
                user.Id,
                dto.JobTitle,
                dto.DepartmentId,
                dto.HireDate
            );

            await repo.AddAsync(employee);


            return Result<bool>.Success(true, localizer["Operationcompletedsuccessfully"]);
        }
        catch (Exception ex)
        {
            return Result<bool>.Error(
                $"{localizer["FailedToAssignEmployee"]}: {ex.Message}",
                500
            );
        }
    }
}