using Application.DTO.AuthModules;
using MediatR;

namespace Application.Commands.AuthModules
{
 
        public record RefreshTokenCommand(string RefreshToken)
            : IRequest<Result<RefreshTokenResponseDto>>;
    }

