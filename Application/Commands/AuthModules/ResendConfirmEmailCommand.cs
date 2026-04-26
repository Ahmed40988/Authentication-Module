using MediatR;

namespace Application.Commands.AuthModules
{
    public record ResendConfirmationEmailCommand(string Email)
      : IRequest<Result<bool>>;
}
