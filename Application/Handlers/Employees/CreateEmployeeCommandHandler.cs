using Application.Commands.Employees;
using Application.DTO.Employees;
using Domain.Entities.AuthModules;
using Domain.Entities.Departments;
using Domain.Entities.Employees;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers.Employees;

public class CreateEmployeeCommandHandler(
    IGenericRepositories<Employee> repo,
    IGenericRepositories<Department> departmentRepo,
    UserManager<User> userManager,
    IStringLocalizer localizer
) : IRequestHandler<CreateEmployeeCommand, Result<string>>
{
    private readonly IGenericRepositories<Employee> repo = repo;
    private readonly IGenericRepositories<Department> departmentRepo = departmentRepo;
    private readonly UserManager<User> userManager = userManager;
    private readonly IStringLocalizer localizer = localizer;

    public async Task<Result<string>> Handle(
        CreateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var dto = request.Dto;
            var departmentExists = await departmentRepo.AnyAsync(
                x => x.Id == dto.DepartmentId);
            if (!departmentExists)
                return Result<string>.Failure( localizer["DepartmentNotFound"],   404);

            var existingUser = await userManager.FindByEmailAsync(dto.Email);
            if (existingUser is not null)
                return Result<string>.Failure( localizer["EmailAlreadyExists"], 400);

            var user = new User
            {
                FullName = dto.FullName.Trim(),
                Email = dto.Email.Trim(),
                UserName = dto.Email.Trim(),
                PhoneNumber = dto.PhoneNumber.Trim(),
                EmailConfirmed= true

            };

            var createUserResult = await userManager.CreateAsync(
                user,
                dto.Password);

            if (!createUserResult.Succeeded)
            {
                var error = createUserResult.Errors
                    .FirstOrDefault()?.Description ?? localizer["OperationFailed"];

                return Result<string>.Failure(error,400);
            }

            if (!await userManager.IsInRoleAsync(user, "Employee"))
                await userManager.AddToRoleAsync(user, "Employee");

            var employee = new Employee(
                user.Id,
                dto.JobTitle,
                dto.DepartmentId,
                dto.HireDate
            );

            await repo.AddAsync(employee);

            return Result<string>.Success(
                employee.Id,localizer["Operationcompletedsuccessfully"]
            );
        }
        catch (Exception ex)
        {
            return Result<string>.Error(
                $"{localizer["FailedToCreateEmployee"]}: {ex.Message}",
                500
            );
        }
    }
}