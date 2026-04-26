using MediatR;

namespace Application.Commands.AuthModules
{
    public record ForgotPasswordCommand(string Email)
        : IRequest<Result<bool>>;
}