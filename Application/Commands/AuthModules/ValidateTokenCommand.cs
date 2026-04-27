using Application.DTO.Auth;

namespace Application.Commands.AuthModules
{
    public record ValidateTokenCommand() : IRequest<Result<ValidateTokenResponseDto>>;
}