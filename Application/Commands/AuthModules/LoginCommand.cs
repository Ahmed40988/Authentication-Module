using Application.DTO.Auth;
using Application.DTO.AuthModules;
using MediatR;

namespace Application.Commands.AuthModules
{
    public record LoginCommand(string Email, string Password)
        : IRequest<Result<LoginResponseDto>>;
}