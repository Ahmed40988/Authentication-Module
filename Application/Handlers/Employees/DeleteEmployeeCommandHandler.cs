using Application.Commands.Employees;
using Domain.Entities.Employees;

namespace Application.Handlers.Employees;

public class DeleteEmployeeCommandHandler(
    IGenericRepositories<Employee> repo,
    IStringLocalizer localizer
) : IRequestHandler<DeleteEmployeeCommand, Result<bool>>
{
    private readonly IGenericRepositories<Employee> repo = repo;
    private readonly IStringLocalizer localizer = localizer;

    public async Task<Result<bool>> Handle(
        DeleteEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var employee = await repo.Query ()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (employee is null)
                return Result<bool>.Failure(
                    localizer["EmployeeNotFound"],
                    404);
                await repo.DeleteAsync(employee);    

            return Result<bool>.Success( true, localizer["Operationcompletedsuccessfully"] );
        }
        catch (Exception ex)
        {
            return Result<bool>.Error(
                $"{localizer["FailedToDeleteEmployee"]}: {ex.Message}",
                500
            );
        }
    }
}