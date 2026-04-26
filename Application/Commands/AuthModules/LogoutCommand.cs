using MediatR;

namespace Application.Commands.AuthModules
{
    public record LogoutCommand(string RefreshToken)
        : IRequest<Result<bool>>;
}