
namespace Application.Commands.Employees
{

    public record DeleteEmployeeCommand(string Id)
        : IRequest<Result<bool>>;
}