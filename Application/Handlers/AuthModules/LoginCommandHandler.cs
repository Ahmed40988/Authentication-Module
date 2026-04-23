using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands.AuthModules;
using Application.DTO.Auth;
using Application.Interfaces;
using Application.Interfaces.Auth;
using Domain.Entities.AuthModules;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ServiceStack.Auth;

namespace Application.Handlers.AuthModules
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponseDTO>>
    {
        private readonly UserManager<User> _manager;
        private readonly IStringLocalizer _localizer;
        private readonly ITokenService _tokenService;
        private readonly Interfaces.Auth.IAuthRepository _authRepository;

        public LoginCommandHandler(IStringLocalizer localizer, UserManager<User> manager, ITokenService tokenService, Interfaces.Auth.IAuthRepository authRepository)
        {
            _localizer = localizer;
            _manager = manager;
            _tokenService = tokenService;
            _authRepository = authRepository;
        }
        public async Task<Result<LoginResponseDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _manager.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return Result<LoginResponseDTO>.Error(_localizer["InvalidCredentials"], 401);

            var isPasswordValid = await _manager.CheckPasswordAsync(user, request.Password);
            
            
            if (!isPasswordValid)
                return Result<LoginResponseDTO>.Error(_localizer["InvalidCredentials"], 401);

            var tokenResult = await _tokenService.GenerateJwtToken(user);

            var refreshToken = await _tokenService.GenerateRefreshToken(user);

            var roles = await _manager.GetRolesAsync(user);
            var roleName = roles.FirstOrDefault();


            List<string> permissionNames;
            if (roleName == "SuperAdmin")
            {
                permissionNames = await _authRepository.GetAllPermissionNamesAsync();
            }
            else
            {
                var role = await _authRepository.GetRoleByNameAsync(roleName);
                var permissions = await _authRepository.GetPermissionsByRoleIdAsync(role.Id);   
                permissionNames = permissions.Select(p => p.Name).ToList();
            }

            var response = new LoginResponseDTO
            {
                // Tokens
                Token = tokenResult.Token,
                TokenExpiresAt = tokenResult.ExpiresAt,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiresAt = refreshToken.ExpiresOn,

                Role = new RoleDTO { Id = roleName ?? string.Empty, Name = roleName ?? string.Empty },
                Permissions = permissionNames,

                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,

            };

            return Result<LoginResponseDTO>.Success(
                response,
                _localizer["LoginSuccessfully"],
                200);

        }
    }
}
