using Application.DTO.Auth;

namespace Application.Commands.AuthModules
{
    public record CreateRoleCommand(
       string name
    ) : IRequest<Result<string>>;
}
