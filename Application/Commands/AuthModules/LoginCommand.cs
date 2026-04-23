using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Auth;
using Domain.Entities.AuthModules;
using MediatR;

namespace Application.Commands.AuthModules
{
    public class LoginCommand : IRequest<Result<LoginResponseDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
